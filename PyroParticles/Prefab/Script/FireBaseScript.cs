﻿using UnityEngine;
using System.Collections;
using UnityEngine.AI;

namespace DigitalRuby.PyroParticles
{
    [System.Serializable]
    public struct RangeOfIntegers
    {
        public int Minimum;
        public int Maximum;
    }

    [System.Serializable]
    public struct RangeOfFloats
    {
        public float Minimum;
        public float Maximum;
    }

    public class FireBaseScript : MonoBehaviour
    {
        [Tooltip("Optional audio source to play once when the script starts.")]
        public AudioSource AudioSource;

        [Tooltip("How long the script takes to fully start. This is used to fade in animations and sounds, etc.")]
        public float StartTime = 1.0f;

        [Tooltip("How long the script takes to fully stop. This is used to fade out animations and sounds, etc.")]
        public float StopTime = 3.0f;

        [Tooltip("How long the effect lasts. Once the duration ends, the script lives for StopTime and then the object is destroyed.")]
        public float Duration = 2.0f;

        [Tooltip("How much force to create at the center (explosion), 0 for none.")]
        public float ForceAmount;

        [Tooltip("The radius of the force, 0 for none.")]
        public float ForceRadius;

        [Tooltip("A hint to users of the script that your object is a projectile and is meant to be shot out from a person or trap, etc.")]
        public bool IsProjectile;

        [Tooltip("Particle systems that must be manually started and will not be played on start.")]
        public ParticleSystem[] ManualParticleSystems;

        private float startTimeMultiplier;
        private float startTimeIncrement;

        private float stopTimeMultiplier;
        private float stopTimeIncrement;

        private IEnumerator CleanupEverythingCoRoutine()
        {
            // 2 extra seconds just to make sure animation and graphics have finished ending
            yield return new WaitForSeconds(StopTime + 2.0f);

            GameObject.Destroy(gameObject);
        }

        private void StartParticleSystems()
        {
            foreach (ParticleSystem p in gameObject.GetComponentsInChildren<ParticleSystem>())
            {
                if (ManualParticleSystems == null || ManualParticleSystems.Length == 0 ||
                    System.Array.IndexOf(ManualParticleSystems, p) < 0)
                {
                    if (p.main.startDelay.constant == 0.0f)
                    {
                        // wait until next frame because the transform may change
                        var m = p.main;
                        var d = p.main.startDelay;
                        d.constant = 0.01f;
                        m.startDelay = d;
                    }
                    p.Play();
                }
            }
        }

        protected virtual void Awake()
        {
            Starting = true;
            //int fireLayer = UnityEngine.LayerMask.NameToLayer("FireLayer");
           // UnityEngine.Physics.IgnoreLayerCollision(fireLayer, fireLayer);
        }

        protected virtual void Start()
        {
            if (AudioSource != null)
            {
                AudioSource.Play();
            }

            // precalculate so we can multiply instead of divide every frame
            stopTimeMultiplier = 1.0f / StopTime;
            startTimeMultiplier = 1.0f / StartTime;

            // if this effect has an explosion force, apply that now
            CreateExplosion(gameObject.transform.position, ForceRadius, ForceAmount);

            // start any particle system that is not in the list of manual start particle systems
            StartParticleSystems();

            // If we implement the ICollisionHandler interface, see if any of the children are forwarding
            // collision events. If they are, hook into them.
            ICollisionHandler handler = (this as ICollisionHandler);
            if (handler != null)
            {
                FireCollisionForwardScript collisionForwarder = GetComponentInChildren<FireCollisionForwardScript>();
                if (collisionForwarder != null)
                {
                    collisionForwarder.CollisionHandler = handler;
                }
            }
        }

        protected virtual void Update()
        {
            // reduce the duration
            Duration -= Time.deltaTime;
            if (Stopping)
            {
                // increase the stop time
                stopTimeIncrement += Time.deltaTime;
                if (stopTimeIncrement < StopTime)
                {
                    StopPercent = stopTimeIncrement * stopTimeMultiplier;
                }
            }
            else if (Starting)
            {
                // increase the start time
                startTimeIncrement += Time.deltaTime;
                if (startTimeIncrement < StartTime)
                {
                    StartPercent = startTimeIncrement * startTimeMultiplier;
                }
                else
                {
                    Starting = false;
                }
            }
            else if (Duration <= 0.0f)
            {
                // time to stop, no duration left
                Stop();
            }
        }

        public static void CreateExplosion(Vector3 pos, float radius, float force)
        {
            if (force <= 0.0f || radius <= 0.0f)
            {
                return;
            }
				

            // find all colliders and add explosive force
            Collider[] objects = UnityEngine.Physics.OverlapSphere(pos, radius);
            foreach (Collider h in objects)
            {
				//Baby独自処理
				if (h.GetComponent<ForceTest> () != null) {
					h.GetComponent<ForceTest> ().ChangeRagdoll ();

					if (h.GetComponent<ControllManager> () != null) {

						if (h.GetComponent<ControllManager> ().IsDead == false) {

							GameObject.Find("Canvas").GetComponent<GameManager> ().ActivateRestartUi ();

						}

						h.GetComponent<ControllManager> ().IsDead = true;

						GameObject.Find("Canvas").GetComponent<GameManager> ().Achievement_NoDown = false;
						GameObject.Find("Canvas").GetComponent<GameManager> ().StopCoroutine ("CountTimer");
						GameManager.Pad_Base.SetActive (false);
						GameManager.Pad_Stick.SetActive (false);
					}

					if (h.GetComponent<NavMeshAgent> () != null) {
						h.GetComponent<NavMeshAgent> ().enabled = false;
						h.GetComponent<EnemyMove> ().enabled = false;
					}
				}

                Rigidbody r = h.GetComponent<Rigidbody>();
                if (r != null)
                {
                    r.AddExplosionForce(force * 2f, pos, radius);
                }
            }
        }

        public virtual void Stop()
        {
            if (Stopping)
            {
                return;
            }
            Stopping = true;

            // cleanup particle systems
            foreach (ParticleSystem p in gameObject.GetComponentsInChildren<ParticleSystem>())
            {
                p.Stop();
            }

            StartCoroutine(CleanupEverythingCoRoutine());
        }

        public bool Starting
        {
            get;
            private set;
        }

        public float StartPercent
        {
            get;
            private set;
        }

        public bool Stopping
        {
            get;
            private set;
        }

        public float StopPercent
        {
            get;
            private set;
        }
    }
}
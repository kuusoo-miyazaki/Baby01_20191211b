using UnityEngine;
using NendUnityPlugin.AD.Video;

public class VideoObject : MonoBehaviour
{
	#if UNITY_IOS
	private string spotId = "802557", apiKey = "b6a97b05dd088b67f68fe6f155fb3091f302b48b";
	#else
	private string spotId = "980376", apiKey = "d55824b99f3f76553eaab00e813a624d4ed85f86";
	#endif
	private NendAdInterstitialVideo m_InterstitialVideoAd;

	// Use this for initialization
	void Start ()
	{
		m_InterstitialVideoAd =
			NendAdInterstitialVideo.NewVideoAd (spotId, apiKey);

		m_InterstitialVideoAd.AdLoaded += (instance) => {
			// 広告ロード成功のコールバック
		};
		m_InterstitialVideoAd.AdFailedToLoad += (instance, errorCode) => {
			// 広告ロード失敗のコールバック
		};
		m_InterstitialVideoAd.AdFailedToPlay += (instance) => {
			// 再生失敗のコールバック
		};
		m_InterstitialVideoAd.AdShown += (instance) => {
			// 広告表示のコールバック
		};
		m_InterstitialVideoAd.AdStarted += (instance) => {
			// 再生開始のコールバック
		};
		m_InterstitialVideoAd.AdStopped += (instance) => {
			// 再生中断のコールバック
		};
		m_InterstitialVideoAd.AdCompleted += (instance) => {
			// 再生完了のコールバック
		};
		m_InterstitialVideoAd.AdClicked += (instance) => {
			// 広告クリックのコールバック
		};
		m_InterstitialVideoAd.InformationClicked += (instance) => {
			// オプトアウトクリックのコールバック
		};
		m_InterstitialVideoAd.AdClosed += (instance) => {
			// 広告クローズのコールバック
		};
	}

	void OnDestroy () {
		m_InterstitialVideoAd.Release ();
	}

	public void Load ()
	{
		m_InterstitialVideoAd.Load ();
	}

	public void Show ()
	{
		if (m_InterstitialVideoAd.IsLoaded()) {
			m_InterstitialVideoAd.Show ();
		}
	}
}
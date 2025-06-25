using UnityEngine;

namespace Script
{
    public class MusicPlayer : MonoBehaviour
    {
        private static MusicPlayer instance;

        [SerializeField] private AudioClip musicClip; // Kéo file nhạc vào đây trong Inspector
        private AudioSource audioSource;

        void Awake()
        {
            // Nếu đã có 1 instance rồi (khác this) → huỷ bản mới
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            // Đặt instance
            instance = this;
            DontDestroyOnLoad(gameObject);

            // Cấu hình audio
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
                audioSource = gameObject.AddComponent<AudioSource>();

            if (musicClip != null)
            {
                audioSource.clip = musicClip;
                audioSource.loop = true;
                audioSource.playOnAwake = false;
                audioSource.volume = 0.5f; // tuỳ chỉnh nếu muốn
                audioSource.Play();
            }
            else
            {
                Debug.LogWarning("⚠ Chưa gán file nhạc vào MusicPlayer!");
            }
        }
    }
}
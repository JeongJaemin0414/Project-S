namespace Assets.Script.Utility
{
    public class ISSUE
    {
#if UNITY_IOS || UNITY_IPHONE || UNITY_ANDROID || UNITY_EDITOR
        /// <summary>
        /// UI 캐싱
        /// </summary>
        public static readonly ISSUE UI_CACHING = false;
#else
        /// <summary>
        /// UI 캐싱
        /// </summary>
        public static readonly Issue UI_CACHING = true;
#endif

        private readonly bool value;
        private ISSUE(bool value)
        {
            this.value = value;
        }

        public static implicit operator ISSUE(bool value)
        {
            return new ISSUE(value);
        }

        public static implicit operator bool(ISSUE issue)
        {
            return issue.value;
        }
    }
}
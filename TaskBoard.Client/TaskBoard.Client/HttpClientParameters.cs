namespace TaskBoard.Client {
	public class HttpClientParameters {
		public string ServerAddress { get; set; }
		public int TimeoutMs { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
		public bool IsAuthorize { get; set; }
	}
}
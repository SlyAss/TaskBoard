using TaskBoard.Common.Enums;
using TaskBoard.Common.Tables;

namespace TaskBoard.Client.UI.Extensions {
	public static class UserExtensions {
		public static bool IsHaveAccess(this User user, AccessType accessType) {
			return (user.AccessType & (int)accessType) != 0;
		}

		public static void AddAccess(this User user, AccessType accessType) {
			user.AccessType |= (int)accessType;
		}
	}
}
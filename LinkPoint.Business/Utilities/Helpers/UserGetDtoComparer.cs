using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserDTOs;

namespace LinkPoint.Business.Utilities.Helpers;

public class UserGetDtoComparer : IEqualityComparer<UserGetDto>
{
    public bool Equals(UserGetDto x, UserGetDto y)
    {
        return x.UserId == y.UserId;
    }

    public int GetHashCode(UserGetDto obj)
    {
        return obj.UserId.GetHashCode();
    }
}

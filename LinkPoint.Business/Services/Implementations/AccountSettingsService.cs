using AutoMapper;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.PasswordDTOs;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserAboutDTOs;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserEducationDTOs;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserInterestDTOs;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserWorkDTOs;
using LinkPoint.Business.Services.Interfaces;
using LinkPoint.Business.Utilities.Exceptions.NotFoundException;
using LinkPoint.Business.Utilities.Exceptions.NotFoundExceptions;
using LinkPoint.Core.Entities;
using LinkPoint.Core.Repositories;
using Microsoft.AspNetCore.Identity;

namespace LinkPoint.Business.Services.Implementations;

public class AccountSettingsService : IAccountSettingsService
{
    private readonly IUserEducationRepository _userEducationRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;
    private readonly IUserInterestRepository _userInterestRepository;

    public AccountSettingsService(IUserEducationRepository userEducationRepository,
        UserManager<AppUser> userManager, 
        IMapper mapper,
        IUserInterestRepository userInterestRepository)
    {
        _userEducationRepository = userEducationRepository;
        _userManager = userManager;
        _mapper = mapper;
        _userInterestRepository = userInterestRepository;
    }
    public async Task UpdateUserEducation(string UserId ,UserEducationPutDto userEducationPutDto)
    {
        var user = await _userManager.FindByIdAsync(UserId);
        if (user is null) throw new UserNotFoundException(404, "User is not found");
        var userEducation=await _userEducationRepository.GetSingleAsync(ue=>ue.UserId==UserId);
        if (userEducation is null)
        {
            var userEducationn = _mapper.Map<UserEducation>(userEducationPutDto);
            userEducationn.UserId = UserId; 
            userEducationn.CreatedDate = DateTime.UtcNow;
            userEducationn.UpdatedDate = DateTime.UtcNow;
            await _userEducationRepository.InsertAsync(userEducationn);
            await _userEducationRepository.CommitAsync();
        }
        else
        {
            var userEdu=_mapper.Map(userEducationPutDto, userEducation);
            userEdu.UpdatedDate = DateTime.UtcNow;
            await _userEducationRepository.CommitAsync();
        }

    }
    public async Task<UserEducationGetDto> GetUserEducation(string UserId)
    {
        var user = await _userManager.FindByIdAsync(UserId);
        if (user is null) throw new UserNotFoundException(404, "User is not found");
        var userEducation=await _userEducationRepository.GetSingleAsync(ue=>ue.UserId==UserId);
        var userEducationGetDto=_mapper.Map<UserEducationGetDto>(userEducation);
        return userEducationGetDto;
    }
    public async Task CreateUserInterest(UserInterestPostDto userInterestPostDto)
    {
        //var user = await _userManager.FindByIdAsync(userInterestPostDto.UserId);
        //if (user is null) throw new UserNotFoundException(404, "User is not found");
        //var userInterest=_mapper.Map<UserInterest>(userInterestPostDto);
        //await _userInterestRepository.InsertAsync(userInterest);
        //await _userInterestRepository.CommitAsync();
    }

    public Task CreateUserWork(UserWorkPostDto userWorkPostDto)
    {
        throw new NotImplementedException();
    }

    public Task<List<UserInterestGetDto>> GetAllUserInterests(string UserId)
    {
        throw new NotImplementedException();
    }

    public Task<UserAboutGetDto> GetUserAbout(string UserId)
    {
        throw new NotImplementedException();
    }


    public Task<UserWorkGetDto> GetUserWork(string UserId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateUserAbout(UserAboutPutDto userAboutPostDto)
    {
        throw new NotImplementedException();
    }


    public Task UpdateUserInterest(UserInterestPutDto userInterestPutDto)
    {
        throw new NotImplementedException();
    }

    public Task UpdateUserWork(UserWorkPutDto userWorkPostDto)
    {
        throw new NotImplementedException();
    }
    public Task ChangePassword(ChangePasswordDto changePasswordDto)
    {
        throw new NotImplementedException();
    }
}

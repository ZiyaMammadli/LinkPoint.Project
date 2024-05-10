﻿using AutoMapper;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.PasswordDTOs;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserAboutDTOs;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserEducationDTOs;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserInterestDTOs;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserWorkDTOs;
using LinkPoint.Business.Services.Interfaces;
using LinkPoint.Business.Utilities.Exceptions.CommonExceptions;
using LinkPoint.Business.Utilities.Exceptions.NotFoundException;
using LinkPoint.Business.Utilities.Exceptions.NotFoundExceptions;
using LinkPoint.Business.Utilities.Exceptions.NotValidExceptions;
using LinkPoint.Core.Entities;
using LinkPoint.Core.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;

namespace LinkPoint.Business.Services.Implementations;

public class AccountSettingsService : IAccountSettingsService
{
    private readonly IUserEducationRepository _userEducationRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;
    private readonly IUserInterestRepository _userInterestRepository;
    private readonly IUserWorkRepository _userWorkRepository;
    private readonly IUserAboutRepository _userAboutRepository;

    public AccountSettingsService(IUserEducationRepository userEducationRepository,
        UserManager<AppUser> userManager, 
        IMapper mapper,
        IUserInterestRepository userInterestRepository,
        IUserWorkRepository userWorkRepository,
        IUserAboutRepository userAboutRepository)
    {
        _userEducationRepository = userEducationRepository;
        _userManager = userManager;
        _mapper = mapper;
        _userInterestRepository = userInterestRepository;
        _userWorkRepository = userWorkRepository;
        _userAboutRepository = userAboutRepository;
    }
    public async Task UpdateUserEducation(int Id ,UserEducationPutDto userEducationPutDto)
    {
        if (Id != userEducationPutDto.Id) throw new IdNotValidException(400,"Id is not valid");
        var userEducation=await _userEducationRepository.GetSingleAsync(ue=>ue.Id==Id);
        if (userEducation is null)
        {
            var userEducationn = _mapper.Map<UserEducation>(userEducationPutDto);
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
        var user = await _userManager.FindByIdAsync(userInterestPostDto.UserId);
        if (user is null) throw new UserNotFoundException(404, "User is not found");
        var userInterest = _mapper.Map<UserInterest>(userInterestPostDto);
        await _userInterestRepository.InsertAsync(userInterest);
        await _userInterestRepository.CommitAsync();
    }
    public async Task DeleteUserInterest(int Id, UserInterestDeleteDto userInterestDeleteDto)
    {
        if (Id != userInterestDeleteDto.Id) throw new IdNotValidException(400, "Id is not Valid");
        var UserInterest=await _userInterestRepository.GetByIdAsync(Id);
        _userInterestRepository.Delete(UserInterest);
        await _userInterestRepository.CommitAsync();
    }
    public async Task<List<UserInterestGetDto>> GetAllUserInterests(string UserId)
    {
        var user = await _userManager.FindByIdAsync(UserId);
        if (user is null) throw new UserNotFoundException(404, "User is not found");
        List<UserInterestGetDto> userInterestGetDtos = new List<UserInterestGetDto>();
        List<UserInterest> userInterests= await _userInterestRepository.GetAllAsync(ui=>ui.UserId==UserId);
        foreach (var userInterest in userInterests)
        {
            var userInterestGetDto= _mapper.Map<UserInterestGetDto>(userInterest);
            userInterestGetDtos.Add(userInterestGetDto);
        }
        return userInterestGetDtos;
    }
    public async Task UpdateUserWork(int Id,UserWorkPutDto userWorkPutDto)
    {
        if (Id != userWorkPutDto.Id) throw new IdNotValidException(400, "Id is not valid");
        var userwork= await _userWorkRepository.GetByIdAsync(Id);
        if(userwork is null)
        {
            var userworkk=_mapper.Map<UserWork>(userWorkPutDto);
            userworkk.CreatedDate = DateTime.UtcNow;
            userworkk.UpdatedDate = DateTime.UtcNow;
            await _userWorkRepository.InsertAsync(userworkk);
            await _userWorkRepository.CommitAsync();
        }
        else
        {
            var userWork = _mapper.Map(userWorkPutDto, userwork);
            userWork.UpdatedDate = DateTime.UtcNow;
            await _userWorkRepository.CommitAsync();
        }
    }
    public async Task<UserWorkGetDto> GetUserWork(string UserId)
    {
        var userWork=await _userWorkRepository.GetSingleAsync(uw=>uw.UserId==UserId);
        if (userWork is null) throw new UserWorkNotFoundException(404, "UserWork is not found");
        var userwork=_mapper.Map<UserWorkGetDto>(userWork);
        return userwork;
    }
    public async Task<UserAboutGetDto> GetUserAbout(string UserId)
    {
        var userAbout= await _userAboutRepository.GetSingleAsync(ua=>ua.UserId==UserId);
        if (userAbout is null) throw new UserAboutNotFoundException(404, "UserAbut is not found");
        var userAboutGetDto=_mapper.Map<UserAboutGetDto>(userAbout);
        return userAboutGetDto;
    }
    public async Task UpdateUserAbout(int Id,UserAboutPutDto userAboutPutDto)
    {
        if (Id != userAboutPutDto.Id) throw new IdNotValidException(400, "Id is not valid");
        var CurrentUserAbout=await _userAboutRepository.GetByIdAsync(userAboutPutDto.Id);
        if(CurrentUserAbout is null)
        {
            var userAbout=_mapper.Map<UserAbout>(userAboutPutDto);
            userAbout.CreatedDate = DateTime.UtcNow;
            userAbout.UpdatedDate = DateTime.UtcNow;
            await _userAboutRepository.InsertAsync(userAbout);
            await _userAboutRepository.CommitAsync();
        }
        else
        {
            var UserAbout = _mapper.Map(userAboutPutDto, CurrentUserAbout);
            UserAbout.UpdatedDate= DateTime.UtcNow;
            await _userAboutRepository.CommitAsync();
        }
    }
    public async Task ChangePassword(string UserId,ChangePasswordDto changePasswordDto)
    {
        if (UserId != changePasswordDto.UserId) throw new IdNotValidException(400, "UserId is not valid");
        var user=await _userManager.FindByIdAsync(changePasswordDto.UserId);
        var result=await _userManager.CheckPasswordAsync(user, changePasswordDto.OldPassword);
        if(result is false) throw new InvalidCredentialsException(401, "Incorrect password");
        await _userManager.ChangePasswordAsync(user,changePasswordDto.OldPassword,changePasswordDto.NewPassword);
    }


}

using Application.Features.Auth.Constants;
using Application.Services.Repositories;
using Domain.Entities;
using Domain.Enums;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using NArchitecture.Core.Security.Enums;
using NArchitecture.Core.Security.Hashing;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Application.Features.Auth.Rules;

public class AuthBusinessRules : BaseBusinessRules
{
    private readonly IUserRepository _userRepository;
    private readonly ILocalizationService _localizationService;

    public AuthBusinessRules(IUserRepository userRepository, ILocalizationService localizationService)
    {
        _userRepository = userRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, AuthMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task EmailAuthenticatorShouldBeExists(EmailAuthenticator? emailAuthenticator)
    {
        if (emailAuthenticator is null)
            await throwBusinessException(AuthMessages.EmailAuthenticatorDontExists);
    }

    public async Task OtpAuthenticatorShouldBeExists(OtpAuthenticator? otpAuthenticator)
    {
        if (otpAuthenticator is null)
            await throwBusinessException(AuthMessages.OtpAuthenticatorDontExists);
    }

    public async Task OtpAuthenticatorThatVerifiedShouldNotBeExists(OtpAuthenticator? otpAuthenticator)
    {
        if (otpAuthenticator is not null && otpAuthenticator.IsVerified)
            await throwBusinessException(AuthMessages.AlreadyVerifiedOtpAuthenticatorIsExists);
    }

    public async Task EmailAuthenticatorActivationKeyShouldBeExists(EmailAuthenticator emailAuthenticator)
    {
        if (emailAuthenticator.ActivationKey is null)
            await throwBusinessException(AuthMessages.EmailActivationKeyDontExists);
    }

    public async Task UserShouldBeExistsWhenSelected(User? user)
    {
        if (user == null)
            await throwBusinessException(AuthMessages.UserDontExists);
    }

    public async Task UserShouldNotBeHaveAuthenticator(User user)
    {
        if (user.AuthenticatorType != AuthenticatorType.None)
            await throwBusinessException(AuthMessages.UserHaveAlreadyAAuthenticator);
    }

    public async Task RefreshTokenShouldBeExists(RefreshToken? refreshToken)
    {
        if (refreshToken == null)
            await throwBusinessException(AuthMessages.RefreshDontExists);
    }

    public async Task RefreshTokenShouldBeActive(RefreshToken refreshToken)
    {
        if (refreshToken.RevokedDate != null && DateTime.UtcNow >= refreshToken.ExpirationDate)
            await throwBusinessException(AuthMessages.InvalidRefreshToken);
    }

    public async Task UserEmailShouldBeNotExists(string email)
    {
        bool doesExists = await _userRepository.AnyAsync(predicate: u => u.Email == email);
        if (doesExists)
            await throwBusinessException(AuthMessages.UserMailAlreadyExists);
    }

    public async Task UserPasswordShouldBeMatch(User user, string password)
    {
        if (!HashingHelper.VerifyPasswordHash(password, user!.PasswordHash, user.PasswordSalt))
            await throwBusinessException(AuthMessages.PasswordDontMatch);
    }

    public async Task UserIdentityShouldBeNotExists(string identityNumber)
    {
        HashingHelper.CreatePasswordHash(
                    identityNumber,
                    passwordHash: out byte[] identityHash,
                    passwordSalt: out byte[] identitySalt
                );

        bool hashDoesExists = await _userRepository.AnyAsync(predicate: u => u.IdentityNumberHash == identityHash);
        bool saltDoesExists = await _userRepository.AnyAsync(predicate: u => u.IdentityNumberSalt== identitySalt);

        if (hashDoesExists == true || saltDoesExists == true)
            await throwBusinessException(AuthMessages.IdentityHasExists);

    }

    public async Task IdentityNumberIsAccurate(string identityNumber)
    {
        if (string.IsNullOrEmpty(identityNumber) || identityNumber.Length != 11 || !long.TryParse(identityNumber, out _))
            await throwBusinessException(AuthMessages.IdentityHasExists);

        if (identityNumber[0] == '0')
            await throwBusinessException(AuthMessages.IdentityHasExists);

        int[] digits = identityNumber.Select(c => c - '0' ).ToArray();

        int oddSum = digits[0] + digits[2] + digits[4] + digits[6] + digits[8];
        int evenSum = digits[1] + digits[3] + digits[5] + digits[7];
        int tenthDigit = ((7 * oddSum) - evenSum) % 10;

        if (tenthDigit != digits[9])
            await throwBusinessException(AuthMessages.IdentityHasExists);

        int totalSum = oddSum + evenSum + digits[9];
        int eleventhDigit = totalSum % 10;

        if(eleventhDigit != digits[10])
            await throwBusinessException(AuthMessages.IdentityHasExists);

    }

    public async Task UserShouldBeConfirmedForLogin(User user)
    {
        switch (user.UserState)
        {
            case UserState.Pending:
                await throwBusinessException(AuthMessages.UserUserStateIsPending);
                break;
            case UserState.Rejected:
                await throwBusinessException(AuthMessages.UserUserStateIsRejected);
                break;
            case UserState.BlackList:
                await throwBusinessException(AuthMessages.UserUserStateIsBlackList);
                break;
            default:
                break;
        }
    }
}

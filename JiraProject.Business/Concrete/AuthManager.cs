using System.Linq;
using System.Security.Claims;
using AutoMapper;
using JiraProject.Business.Abstract;
using JiraProject.Business.BusinessResultObjects;
using JiraProject.Business.Security.Hashing;
using JiraProject.Business.Security.JWT;
using JiraProject.DataAccess.Abstract;
using JiraProject.Entities.DataTransferObjects.Request;
using JiraProject.Entities.Entities;
using Microsoft.AspNetCore.Http;

namespace JiraProject.Business.Concrete
{
    public class AuthManager : IAuthService
    {

        private IUserRepository _userRepository;
        private IMapper _mapper;
        private TokenHelper _tokenHelper;
        private IRoleRepository _roleRepository;
        private HttpContext _httpContext;

        public AuthManager(IUserRepository userRepository, IMapper mapper, TokenHelper tokenHelper, IRoleRepository roleRepository, IHttpContextAccessor httpContext)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenHelper = tokenHelper;
            _roleRepository = roleRepository;
            _httpContext = httpContext.HttpContext;
        }



        public IBusinessResult Register(RegisterRequest registerRequest)
        {
            byte[] passwordHash;
            byte[] passwordSalt;

            HashHelper.CreatePasswordHash(registerRequest.Password, out passwordHash, out passwordSalt);

            var user = _mapper.Map<User>(registerRequest);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _userRepository.Add(user);
            _userRepository.Save();
            return new SuccessResult("Başarıyla Üye Olundu.");
        }




        public IBusinessDataResult<AccessToken> Login(LoginRequest loginRequest)
        {
            var user = _userRepository.Get(x => x.Email == loginRequest.Email);

            if (user != null)
            {
                bool confirmPassword = HashHelper.VerifyPasswordHash(loginRequest.Password, user.PasswordHash, user.PasswordSalt);

                if (confirmPassword)
                {
                   var jwtToken = _tokenHelper.CreateToken(user, _roleRepository.GetUserRoles(user).ToList());
                   return new SuccessDataResult<AccessToken>(jwtToken,"Giriş Başarılı.");
                }

                return new ErrorDataResult<AccessToken>("Şifre Yanlış.");
            }

            return new ErrorDataResult<AccessToken>("Kullanıcı Bulunamadı.");
        }



        public IBusinessDataResult<User> GetAuthenticatedUser()
        {
            if (_httpContext.User.Identity.IsAuthenticated)
            {
                int userId = int.Parse(_httpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                return  new SuccessDataResult<User>(_userRepository.Get(x => x.Id == userId));
            }

            return new ErrorDataResult<User>();
        }
    }
}
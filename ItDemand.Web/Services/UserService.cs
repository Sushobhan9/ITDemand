using AutoMapper;
using ItDemand.Domain.DataContext;
using ItDemand.Domain.Models;
using ItDemand.Web.Models;
using ItDemand.Web.ViewModels;

namespace ItDemand.Web.Services
{
	public class UserService
	{
		private readonly ItDemandContext _db;
		private readonly IMapper _mapper;

		public UserService(ItDemandContext context, IMapper mapper)
		{
			_db = context;
			_mapper = mapper;
		}

		public User Add(User user)
		{
            user.Created = DateTimeOffset.Now;
            _db.Users.Add(user);
			//_db.SaveChanges();
			return user;
		}

        public User? GetEmployeeByUserName(string userName)
		{
			var user = _db.Users
				.SingleOrDefault(x => x.UserName.ToLower() == userName.ToLower());
			return user;
		}

        public User? GetEmployeeById(int id)
        {
            var user = _db.Users
                .SingleOrDefault(x => x.Id == id);
            return user;
        }

        public User? VerifyOrCreateUser(ActiveDirectoryUser? adUser)
		{
			if (adUser == null) return null;
			if (string.IsNullOrEmpty(adUser.UserName)) return null;

			var user = GetEmployeeByUserName(adUser.UserName);
			if (user != null) return user;

			// The user does not yet exist, create a new User record
			user = new User();
			_mapper.Map(adUser, user);
			user.Created = DateTimeOffset.Now;
			Add(user);
			return user;
		}

        public User? VerifyOrCreateUser(UserViewModel? userViewModel)
        {
            if (userViewModel == null) return null;
            if (string.IsNullOrEmpty(userViewModel.UserName)) return null;

            var user = GetEmployeeByUserName(userViewModel.UserName);
            if (user != null) return user;

            // The user does not yet exist, create a new User record
            user = new User();
            _mapper.Map(userViewModel, user);
            Add(user);
            return user;
        }

		//private User? VerifyOrCreateEmployee(string userName)
		//{
  //          if (string.IsNullOrEmpty(userName)) return null;

  //          var user = GetEmployeeByUserName(userName);
  //          if (user != null) return user;

  //          // The user does not yet exist, create a new User record
  //          user = new User();
  //          _mapper.Map(userViewModel, user);
  //          user.Created = DateTimeOffset.Now;
  //          Add(user);
  //          return user;
  //      }
    }
}

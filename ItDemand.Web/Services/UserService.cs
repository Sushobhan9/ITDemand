using AutoMapper;
using Dapper;
using ItDemand.Domain.DataContext;
using ItDemand.Domain.Models;
using ItDemand.Web.Models;
using ItDemand.Web.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using static Dapper.SqlMapper;

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
            user.IsActive = true;
            _db.Users.Add(user);
			return user;
		}

        public User Add(UserViewModel user)
        {
            var entity = new User();
            _mapper.Map(user, entity);
            Add(entity);
            return entity;
        }

        public User Update(UserViewModel user)
        {
            var entity = GetUserById(user.Id);
            _mapper.Map(user, entity);
            entity.LastModified = DateTimeOffset.Now;
            return entity;
        }

        public User? GetUserByUserName(string userName)
		{
			var user = _db.Users
				.SingleOrDefault(x => x.UserName.ToLower() == userName.ToLower());
			return user;
		}

        public User? GetUserById(int id)
        {
            var user = _db.Users
                .SingleOrDefault(x => x.Id == id);
            return user;
        }

        public User? VerifyOrCreateUser(ActiveDirectoryUser? adUser)
		{
			if (adUser == null) return null;
			if (string.IsNullOrEmpty(adUser.UserName)) return null;

			var user = GetUserByUserName(adUser.UserName);
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

            var user = GetUserByUserName(userViewModel.UserName);
            if (user != null) return user;

            // The user does not yet exist, create a new User record
            user = new User();
            _mapper.Map(userViewModel, user);
            Add(user);
            return user;
        }

        public int GetUserCount()
        {
            var sql = @"
                select sum([rows])
                from sys.partitions
                where object_id=object_id('Users')
                and index_id in (0,1)";

            using var conn = new SqlConnection(_db.Database.GetDbConnection().ConnectionString);
            var result = conn.QueryFirstOrDefault<int>(sql);
            return result;
        }

        public async Task<IEnumerable<User>> GetPagedUsers(
            int offset, int pageSize, string sortColumn, string sortDirection, string searchValue)
        {
            var sql = @$"
                select Id, 
                       DisplayName, 
                       UserName, 
                       Email, 
                       SecurityRole, 
                       IsActive, 
                       Created, 
                       LastModified
                from Users
                where DisplayName like '%{searchValue}%' or UserName like '%{searchValue}%'
                order by {sortColumn} {sortDirection}
                offset {offset} rows
                fetch next {pageSize} rows only";

            using var conn = new SqlConnection(_db.Database.GetDbConnection().ConnectionString);
            var result = await conn.QueryAsync<User>(sql);
            return result;
        }
    }
}

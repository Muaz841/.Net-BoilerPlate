using BoilerPlate.Application.Entities;
using BoilerPlate.Application.Shared.InterFaces;
using BoilerPlate.Application.Shared.InterFaces.UserInterface;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BoilerPlate.Infrastructure.Repository
{
    public class PermissionService  : IPermissionService
    {
        private readonly IUserService _userRepository;
        private readonly IDistributedCache _cache;
        private const string CacheKeyPrefix = "permissions";

        public PermissionService(
        IUserService userRepository,
        IDistributedCache cache)
        {
            _userRepository = userRepository;
            _cache = cache;
        }


        public async Task<IReadOnlyList<string>> GetEffectivePermissionsAsync(Guid userId)
        {            
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) return Array.Empty<string>();

            string cacheKey = $"{CacheKeyPrefix}:{userId}:{user.PermissionVersion}";

            var cached = await _cache.GetStringAsync(cacheKey);
            if (cached is not null)
                return JsonSerializer.Deserialize<string[]>(cached)!;

            
            var userRoles = await _userRepository.GetUserRolesAsync(userId);
            var userPermissions = await _userRepository.GetUserPermissionsAsync(userId);

            var permissions = new HashSet<string>();

            
            foreach (var ur in userRoles)
            {
                var rolePermissions = await _userRepository.GetRolePermissionsAsync(ur.RoleId);
                foreach (var rp in rolePermissions.Where(rp => rp.IsGranted))
                    permissions.Add(rp.Permission.Name);
            }
            
            foreach (var up in userPermissions.Where(up => up.IsGranted))
                permissions.Add(up.Permission.Name);

            var result = permissions.OrderBy(p => p).ToArray();

            await _cache.SetStringAsync(cacheKey,
                JsonSerializer.Serialize(result),
                new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(4) });

            return result;
        }


        public async Task InvalidateCacheAsync(Guid userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            var updatedUser = user with { PermissionVersion = user.PermissionVersion + 1 };
            await _userRepository.UpdateUserAsync(user);
        }
    }
}

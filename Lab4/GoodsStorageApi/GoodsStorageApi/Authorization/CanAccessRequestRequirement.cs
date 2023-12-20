using GoodsStorage.DAL.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GoodsStorage.API.Authorization
{
    public class CanAccessRequestRequirement : IAuthorizationRequirement
    {
    }
}

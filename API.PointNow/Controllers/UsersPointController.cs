using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Point.API.Domain.Interfaces.Repositories;
using Point.API.Domain.Services;
using Point.API.Identity.Dtos;
using Point.API.Identity.Entitites;
using Point.API.Presentation.Controllers.Base;
using Point.Domain.DTOs;
using Point.Domain.Enumerators;
using System.Security.Claims;

namespace Point.API.Presentation.Controllers
{
    public class UsersPointController : BaseController
    {
        private readonly IUsersPointService _userPointService;

        public UsersPointController(IUsersPointService userPointService)
        {
            _userPointService = userPointService;
        }

        #region Methods


        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAll()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Guid guidUserId;
            Guid.TryParse(userId, out guidUserId);

            var result = await _userPointService.FindAsync(x => x.UserId == guidUserId);
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Insert(UserPointDto model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Guid guidUserId;
            Guid.TryParse(userId, out guidUserId);

            var errorMessages = await VerifyPoint(model, guidUserId);
            if (errorMessages != string.Empty)
                return BadRequest(errorMessages);

            var userPoint = new UserPoint(guidUserId, model.PointType);
            var result = await _userPointService.InsertAsync(userPoint);
           
            return Ok(result);
        }
        #endregion

        private async Task<string> VerifyPoint(UserPointDto model, Guid userId)
        {
            if (model.PointType == PointTypeEnum.Entrada)
            {
                var hasExit = await _userPointService.FindAsync(x => x.UserId == userId && x.DateHour.Date == DateTime.Now.Date && x.PointType == PointTypeEnum.Saida);
                if (hasExit != null && hasExit.Count() > 0)
                    return "Seu expediente já acabou, ponto de saida já resgistrado";
            }
            
            if (model.PointType == PointTypeEnum.Almoco)
            {
                var hasEnter = await _userPointService.FindAsync(x => x.UserId == userId && x.DateHour.Date == DateTime.Now.Date && x.PointType == PointTypeEnum.Entrada);
                var isLunch = await _userPointService.FindAsync(x => x.UserId == userId && x.DateHour.Date == DateTime.Now.Date && x.PointType == PointTypeEnum.Almoco);
                if (hasEnter == null || hasEnter.Count() == 0)
                    return "Não é possivel sair para almoço, não existe registro de entrada";
                
                if (isLunch != null && isLunch.Count() > 0)
                    return "Saida de almoço já registrada";
            }
            
            if (model.PointType == PointTypeEnum.RetornoAlmoco)
            {
                var hasLunch = await _userPointService.FindAsync(x => x.UserId == userId && x.DateHour.Date == DateTime.Now.Date && x.PointType == PointTypeEnum.Almoco);
                var isReturnLunch = await _userPointService.FindAsync(x => x.UserId == userId && x.DateHour.Date == DateTime.Now.Date && x.PointType == PointTypeEnum.RetornoAlmoco);
                if (hasLunch == null || hasLunch.Count() == 0)
                    return "Não é possivel retornar do almoço, não existe registro de saida para almoço";
                
                if (isReturnLunch != null && isReturnLunch.Count() > 0)
                    return "Retorno de almoço já registrado";
            } 
            
            if (model.PointType == PointTypeEnum.Saida)
            {
                var hasReturnLunch = await _userPointService.FindAsync(x => x.UserId == userId && x.DateHour.Date == DateTime.Now.Date && x.PointType == PointTypeEnum.RetornoAlmoco);
                var isExit = await _userPointService.FindAsync(x => x.UserId == userId && x.DateHour.Date == DateTime.Now.Date && x.PointType == PointTypeEnum.Saida);
                if (hasReturnLunch == null || hasReturnLunch.Count() == 0)
                    return "Não é possivel sair, não existe registro de retorno do almoço";
                
                if (isExit != null && isExit.Count() > 0)
                    return "Saida já registrada";
            }

            return "";
        }
    }
}

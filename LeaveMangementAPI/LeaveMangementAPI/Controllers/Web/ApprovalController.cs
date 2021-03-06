﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeaveMangement_Application.Approval;
using LeaveMangement_Application.Common;
using LeaveMangement_Entity.Dtos;
using LeaveMangement_Entity.Dtos.Approval;
using LeaveMangementAPI.Authorization;
using LeaveMangementAPI.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;

namespace LeaveMangementAPI.Controllers.Web
{
    /// <summary>
    /// 审批
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [EnableCors("any")]
    public class ApprovalController : Controller
    {
        /// <summary>
        /// 审批
        /// </summary>
        /// <returns></returns>
        private readonly IApprovalAppService _approvalAppService;
        private readonly ICommonAppService _commonAppService;
        public IConfiguration _configuration;
        public JWTUtil _jwtUtil = new JWTUtil();
        //signalr
        private ISignalrHubs _signalrHubs;
        public ApprovalController(ISignalrHubs signalrHubs, IServiceProvider service,IApprovalAppService approvalAppService,IConfiguration configuration,ICommonAppService commonAppService)
        {
            _approvalAppService = approvalAppService;
            _commonAppService = commonAppService;
            _configuration = configuration;
            _signalrHubs = signalrHubs;
        }

        //获取当前登录用户的通知数目和内容
        [HttpGet]
        [Authorize]
        public async Task<object> GetInform()
        {
            var context = HttpContext;
            string account = await _jwtUtil.GetMessageByToken(context);
            return _approvalAppService.GetInform(account);
        }
        /// <summary>
        /// 提交请假申请
        /// </summary>
        /// <param name="addApplicationDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<object> AddApplication([FromBody] AddApplicationDto addApplicationDto)
        {
            var context = HttpContext;
            string account = await _jwtUtil.GetMessageByToken(context);
            addApplicationDto.CompId = _commonAppService.GetUserCompId(account);
            addApplicationDto.DeparmentId = _commonAppService.GetUserDepId(account);
            Result result = _approvalAppService.AddApplication(addApplicationDto);
            if (result.Id != 0) {
                await _signalrHubs.Send(result.Id.ToString(), "您有新的消息");
            }
            return result;
        }
        /// <summary>
        /// 获取已提交的请假列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<object> GetApplicationList([FromBody]GetApplicationListDto getApplicationListDto)
        {
            var context = HttpContext;
            getApplicationListDto.Account = await _jwtUtil.GetMessageByToken(context);
            return _approvalAppService.GetApplicationList(getApplicationListDto);
        }
        /// <summary>
        /// 获取一条请假申请的详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public object GetApplicationById(int id)
        {
            return _approvalAppService.GetApplicationById(id);
        }
        /// <summary>
        /// 销假
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public object RevokeApplication(int id)
        {
            return _approvalAppService.RevokeApplication(id);
        }
        /// <summary>
        /// 获取登录用户未提交的申请
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<object> GetUnApplicationList([FromBody]GetApplicationListDto getApplicationListDto)
        {
            var context = HttpContext;
            getApplicationListDto.Account = await _jwtUtil.GetMessageByToken(context);
            return _approvalAppService.GetUnApplicationList(getApplicationListDto);
        }
        /// <summary>
        /// 提交申请
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<object> SubmitApplication(int id)
        {
            Result result = _approvalAppService.SubmitApplication(id);
            if (result.Id != 0)
            {
                await _signalrHubs.Send(result.Id.ToString(), "您有新的消息");
            }
            return result;
        }
        /// <summary>
        /// 编辑未提交的申请
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public object EditApplication([FromBody]EditApplicationDto editApplicationDto)
        {

            return _approvalAppService.EditApplication(editApplicationDto);
        }
        /// <summary>
        /// 删除未提交的申请通过编号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        public object DeleteApplicationById(int id)
        {
            return _approvalAppService.DeleteApplicationById(id);
        }
        /// <summary>
        /// 获取到当前登录的管理员用户待审核的请假申请列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<object> GetCheckingList([FromBody]CheckingDto checkingDto)
        {
            var context = HttpContext;
            checkingDto.Account = await _jwtUtil.GetMessageByToken(context);
            checkingDto.CompId = _commonAppService.GetUserCompId(checkingDto.Account);
            return _approvalAppService.GetCheckingList(checkingDto);
        }
        /// <summary>
        /// 获取到当前登录的管理员用户待审核转正离职列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<object> GetApplyJobList([FromBody]CheckingDto checkingDto)
        {
            var context = HttpContext;
            checkingDto.Account = await _jwtUtil.GetMessageByToken(context);
            checkingDto.CompId = _commonAppService.GetUserCompId(checkingDto.Account);
            return _approvalAppService.GetApplyJobList(checkingDto);
        }
        /// <summary>
        /// 经理或总经理审核请假申请                                                           
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public async Task<object> CheckApplication([FromBody] CheckDto checkDto)
        {
            var context = HttpContext;
            string account = await _jwtUtil.GetMessageByToken(context);
            return _approvalAppService.CheckApplication(checkDto, account);
        }
        /// <summary>
        /// 获取的当前登录用户当月请假次数
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<int> GetApprovalCount()
        {
            var context = HttpContext;
            string account = await _jwtUtil.GetMessageByToken(context);
            int compId = _commonAppService.GetUserCompId(account);
            return _approvalAppService.GetApprovalCount(account,compId);
        }
        /// <summary>
        /// 提交给上一级
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<object> PushCheck([FromBody]PushCheck pushCheck)
        {
            var context = HttpContext;
            string account = await _jwtUtil.GetMessageByToken(context);
            return _approvalAppService.PushCheck(pushCheck, account);
        }

        /// <summary>
        /// 添加转正申请和离职申请
        /// </summary>
        /// <param name="applyJobDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<Result> CreateApplyOfJob([FromBody]ApplyJobDto applyJobDto)
        {
            var context = HttpContext;
            string account = await _jwtUtil.GetMessageByToken(context);
            Result result = _approvalAppService.CreateApplyOfJob(applyJobDto, account);
            if (result.Id != 0)
            {
                await _signalrHubs.Send(result.Id.ToString(), "您有新的消息");
            }
            return result;
        }
    }
}
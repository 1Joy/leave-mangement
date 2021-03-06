﻿using LeaveMangement_Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveMangement_Application.Common
{
    public interface ICommonAppService
    {
        string GetUserAccount(int id);
        int GetUserCompId(string account);
        int GetCompId(string componentName);
        string GetCompName(int compId);
        int GetUserDepId(string account);
        int GetUserId(string userName);
        bool IsExitDep(string depName, int companyId);
        bool IsExitWorker(int? paperType, string paperNumber, int compId);
        int GetDepId(string depName,int compId);
        int GetPaperType(string paperType);
        int GetState(string name, int compId);
        void ChangeDepWorkerCount(List<Worker> workers);
        int GetPosition(string positionName, int compId);
        int GetLeaveCount(string account, int compid);

        bool JudgeAuth(string account, string path);

        int GetManagerPosition(int compId);
        int GetManagerState(int compId);
    }
}

﻿using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProblemAndSolution.Web.Areas.ForPost.Models;
using System.CodeDom;
using static System.Formats.Asn1.AsnWriter;

namespace ProblemAndSolution.Web.Areas.ForPost.Controllers
{
    public class AnswerController : PostBaseController<AnswerController>
    {
        public AnswerController(ILogger<AnswerController> logger,ILifetimeScope lifetimeScope)
            :base(logger,lifetimeScope)
        {

        }    
        
        [Authorize(Roles = "User,Admin")]
        [HttpPost]
        public async Task<IActionResult> AddAnswer(string answerText, int quesId)
        {
            var model = _lifetimeScope.Resolve<AnswerCreateModel>();
            await model.AnswerAsync(answerText, quesId);
            return Ok(model);
        }

        [Authorize(Roles = "User,Admin")]
        [HttpPost]
        public async Task<IActionResult> AddComment(string commentVal, int answerId)
        {
            var model = _lifetimeScope.Resolve<AnswerCreateModel>();

            await model.CommentAsync(commentVal, answerId);
            return Ok(model);
        }

        [Authorize(Roles = "User,Admin")]
        [HttpPost]
        public async Task<IActionResult> AddQuestionVote(int quesId)
        {
            var model = _lifetimeScope.Resolve<AnswerCreateModel>();

            await model.GetQuestionVote(quesId);
            return Ok(model);
        }

        [Authorize(Roles = "User,Admin")]
        [HttpPost]
        public async Task<IActionResult> AddAnsVote(int answerId)
        {
            if (answerId == 0)
                return BadRequest(answerId);

            var model = _lifetimeScope.Resolve<AnswerCreateModel>();

            await model.GetAnsVote(answerId);
            return Ok(model);
        }

    }
}

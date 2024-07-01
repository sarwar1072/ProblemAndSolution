using AutoMapper;
using ProblemAndSolution.Infrastructure.BusinessObj;
using ProblemAndSolution.Infrastructure.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommentEO = ProblemAndSolution.Infrastructure.Entities.Comment;
using VoteEO = ProblemAndSolution.Infrastructure.Entities.Vote;
using AnswerEO = ProblemAndSolution.Infrastructure.Entities.Answer;

namespace ProblemAndSolution.Infrastructure.Services
{
    public class CommentService: ICommentService
    {
        private  IPAndSUnitOfWork _pAndSUnitOfWork;
        private readonly IMapper _mapper;
        private int _qtnvote=0;
        private int _ansvote = 0;
        public CommentService(IPAndSUnitOfWork pAndSUnitOfWork, IMapper mapper)
        {
            _pAndSUnitOfWork = pAndSUnitOfWork;
            _mapper = mapper;           
        }
        private CommentEO MapptoEntity(Comment comment)
        {
            var entity = new CommentEO { 
                AnswerId= comment.AnswerId,
                AuthorName= comment.AuthorName, 
                CreatedBy= comment.CreatedBy,
                CreatedDate=comment.DateTime,
                Description=comment.Description,
                TempId= comment.TempId, 
            };
            return entity;
        }

        public async Task CreateCommentAsync(Comment comment)
        {
            if (comment is null)
                throw new InvalidOperationException("Comment can not be null");

            var entity = MapptoEntity(comment);
            await _pAndSUnitOfWork.CommentRepository.AddAsync(entity);
            await _pAndSUnitOfWork.SaveAsync();
        }

        public async Task<int> GetQusnVote(Guid id,int QuestionId)
        {
            var vote = new VoteEO
            {
                ApplicationUserId=id,
                QuestionId=QuestionId,  
            };
            var count = (await _pAndSUnitOfWork.VoteRepository.GetCountAsync(a => a.ApplicationUserId == id &&
            QuestionId == a.QuestionId));
            if(count == 0)
            {
                await _pAndSUnitOfWork.VoteRepository.AddAsync(vote);
                await _pAndSUnitOfWork.SaveAsync();
                _qtnvote = (await _pAndSUnitOfWork.VoteRepository.GetAsync(
                    a => a.ApplicationUserId == id && a.QuestionId == QuestionId, null)).Count();
                return _qtnvote;
            }
            else
            {
                var voteEO = (await _pAndSUnitOfWork.VoteRepository.GetAsync(a => a.QuestionId == QuestionId &&
                a.ApplicationUserId == id, null)).FirstOrDefault();
                if(voteEO != null)
                {
                    await _pAndSUnitOfWork.VoteRepository.RemoveAsync(voteEO);
                    await _pAndSUnitOfWork.SaveAsync();

                }
                _qtnvote = (await _pAndSUnitOfWork.VoteRepository.GetAsync(a => a.QuestionId == QuestionId, null)).Count();
                return _qtnvote;
            }
        }

        public async Task<int> GetAnsVote(Guid id ,int AnswerId)
        {
            var ans = new VoteEO
            {
                ApplicationUserId=id,
                AnswerId=AnswerId
            };
            var count = (await _pAndSUnitOfWork.VoteRepository.GetCountAsync(a => a.ApplicationUserId == id && a.AnswerId == AnswerId));

            if (count == 0)
            {
                await _pAndSUnitOfWork.VoteRepository.AddAsync(ans);
                await _pAndSUnitOfWork.SaveAsync();
                _ansvote = (await _pAndSUnitOfWork.VoteRepository
                   .GetAsync(c => c.AnswerId == AnswerId, null)).Count();
                return _ansvote;
            }
            else
            {
                var voteEO = (await _pAndSUnitOfWork.VoteRepository
                    .GetAsync(c => c.AnswerId == AnswerId && c.ApplicationUserId == id, null))
                    .FirstOrDefault();

                if (voteEO != null)
                {
                    await _pAndSUnitOfWork.VoteRepository.RemoveAsync(voteEO);
                    await _pAndSUnitOfWork.SaveAsync();
                }
                _ansvote = (await _pAndSUnitOfWork.VoteRepository
                    .GetAsync(c => c.QuestionId == AnswerId, null)).Count();
                return _ansvote;
            }

        }


    }
}

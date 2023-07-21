using AutoMapper;
using ProblemAndSolution.Infrastructure.BusinessObj;
using ProblemAndSolution.Infrastructure.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestionEO = ProblemAndSolution.Infrastructure.Entities.Question;
using TagEO = ProblemAndSolution.Infrastructure.Entities.Tag;
using AnswerEO = ProblemAndSolution.Infrastructure.Entities.Answer;
using CommentEO = ProblemAndSolution.Infrastructure.Entities.Comment;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using ProblemAndSolution.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ProblemAndSolution.Infrastructure.Services
{
    public class QuestionServices:IQuestionServices
    {
        private IPAndSUnitOfWork _AndSUnitOfWork;
        private readonly IMapper _mapper;
        private int _qtnVote;
        public QuestionServices(IPAndSUnitOfWork andSUnitOfWork, IMapper mapper)
        {
            _AndSUnitOfWork = andSUnitOfWork;
            _mapper = mapper;
        }

        private QuestionEO MappToEntity(Question question)
        {
            var entity = new QuestionEO
            {
                Id=question.Id, 
                ApplicationUserId=question.ApplicationUserId,   
                CreatedAt=question.CreatedAt,
                IsSolvedQstn=question.IsSolvedQsn,
                QuestionBody=question.QuestionBody, 
                AuthorName=question.AuthorName,
            };
            entity.Tags = new List<TagEO>();
            entity.Answers = new List<AnswerEO>();
            if(question.Tags != null)
            {
                foreach (var tag in question.Tags)
                {
                    entity.Tags.Add(new TagEO
                    {
                        Name=tag.Name,
                        QuestionId=tag.QuestionId,
                        Id=tag.Id,
                    });
                }
            }

            if (question.Answers != null)
            {
                foreach (var answer in question.Answers)
                {
                    entity.Answers.Add(new AnswerEO
                    {
                        Description=answer.Description,
                        AuthorName = answer.AuthorName,
                        Id=answer.Id,   
                    });

                }
            }
            return entity;
        }

        private Question MappToBusiness(QuestionEO questionEO)
        {
            var business = new Question
            {
                Id= questionEO.Id,  
                ApplicationUserId= questionEO.ApplicationUserId,
                Title=questionEO.Title, 
                IsSolvedQsn=questionEO.IsSolvedQstn,
                CreatedAt=questionEO.CreatedAt,
                QuestionBody= questionEO.QuestionBody,  
                AuthorName=questionEO.AuthorName,
                Temp1=_qtnVote,
            };
            business.Tags = new List<Tag>();
            business.Answers = new List<Answer>();

            if(questionEO.Tags != null)
            {
                foreach (var tag in questionEO.Tags)
                {
                    business.Tags.Add(new Tag
                    {
                        Name=tag.Name,  
                        Id= tag.Id, 
                        QuestionId= tag.QuestionId, 
                    });
                }
            }
            if(questionEO.Answers != null)
            {
                foreach (var answer in questionEO.Answers)
                {
                    var comment = new List<Comment>();
                    if(answer.Comments != null)
                    {
                        foreach (var com in answer.Comments)
                        {
                            comment.Add(new Comment { 
                               Description=com.Description,
                               CreatedBy= com.CreatedBy,
                               DateTime=com.CreatedDate,
                               AuthorName = com.AuthorName, 
                               AnswerId= com.AnswerId,
                               TempId=com.TempId
                            });
                        }
                    }
                    business.Answers.Add(new Answer
                    {
                        Description=answer.Description, 
                        Id= answer.Id,  
                        AuthorName= answer.AuthorName,  
                        CountVote=answer.CountVote,
                        Comments=comment,
                    });
                }               
            }
            return business;
        }

        public async Task CreateQuestion(Question question)
        {
            if(question is null)
            {
                throw new InvalidOperationException("Question can not be null");
            }
            var count = await _AndSUnitOfWork.QuestionRepository.GetCountAsync(x => x.Title == question.Title);
            if(count != 0)
            {
                throw new DuplicateException("Same title is exist.");
            }
            var entity=MappToEntity(question);
            await _AndSUnitOfWork.QuestionRepository.AddAsync(entity);
            await _AndSUnitOfWork.SaveAsync();
        }
        public async Task<Question> GetByIdAsync(int id)
        {
            if (id is 0)
                throw new NullReferenceException("Id must be provided.");

            var entity = (await _AndSUnitOfWork.QuestionRepository.GetAsync(x => x.Id == id, c => c.Include(d => d.Tags))).FirstOrDefault();
            if (entity is null)
                throw new InvalidOperationException("Question table is not found");
            return MappToBusiness(entity);
        }
        public async Task UpdateQuestionAsync(Question question)
        {
            if (question is null)
                throw new InvalidOperationException("Question can not be null");
            var count = await _AndSUnitOfWork.QuestionRepository.GetCountAsync(x => x.Title == question.Title);
            if (count != 0)
                throw new DuplicateException("same questio exist");

            var entity = (await _AndSUnitOfWork.QuestionRepository.GetAsync(x => x.Id == question.Id, b => b.Include(c => c.Tags))).FirstOrDefault();
            if (entity is null)
                throw new InvalidOperationException("No data found");

            AssignQuestion(question, entity);
            await _AndSUnitOfWork.SaveAsync();
        }

        private QuestionEO AssignQuestion(Question question,QuestionEO questionEO)
        {

        }

    }
}

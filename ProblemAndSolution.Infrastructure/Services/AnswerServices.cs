using AutoMapper;
using ProblemAndSolution.Infrastructure.BusinessObj;
using ProblemAndSolution.Infrastructure.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnswerEO = ProblemAndSolution.Infrastructure.Entities.Answer;

namespace ProblemAndSolution.Infrastructure.Services
{
    public class AnswerServices:IAnswerServices
    {
        private IPAndSUnitOfWork _pAndSUnitOfWork;
        private IMapper _mapper;
        public AnswerServices(IPAndSUnitOfWork pAndSUnitOfWork, IMapper mapper)
        {
            _pAndSUnitOfWork = pAndSUnitOfWork;
            _mapper = mapper;   
        }
        public int NumberOfAnswer()
        {
            return _pAndSUnitOfWork.AnswerRepository.GetCount();
        }
        private AnswerEO MappToEntity(Answer answer)
        {
            var entity = new AnswerEO
            {
                AuthorName = answer.AuthorName,
                Description = answer.Description,
                QuestionId = answer.QuestionId,
                TempId = answer.TempId,
            };
            return entity;

        }
        public async Task CreateAnswerAsync(Answer answer)
        {

            if (answer == null)
                throw new InvalidOperationException("Answer can not be null :( ");

                    var answerEntity = MappToEntity(answer);
            await _pAndSUnitOfWork.AnswerRepository.AddAsync(answerEntity); 
            await _pAndSUnitOfWork.SaveAsync();
        }


    }
}

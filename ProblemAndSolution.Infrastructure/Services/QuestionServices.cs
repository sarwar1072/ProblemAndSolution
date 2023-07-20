using AutoMapper;
using ProblemAndSolution.Infrastructure.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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




    }
}

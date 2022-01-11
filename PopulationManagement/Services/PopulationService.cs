// <copyright file="PopulationService.cs" company="Placeholder Company">
// Copyright (c) Placeholder Company. All rights reserved.
// </copyright>

namespace Services
{
    using System;
    using System.Collections.Generic;
    using Core;
    using Models;
    using Models.ViewModels;

    /// <summary>
    /// Access population data.
    /// </summary>
    public class PopulationService : IPopulationService
    {
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="PopulationService"/> class.
        /// </summary>
        /// <param name="unitOfWork">Unit of work instance.</param>
        public PopulationService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get population of many states in one time.
        /// </summary>
        /// <param name="states">List of state id.</param>
        /// <returns>List of states and it's population if exists, otherwise error.</returns>
        public ServiceResult GetPopulationOfManyStates(int[] states)
        {
            try
            {
                List<decimal> populationOfStates = new List<decimal>();
                foreach (int stateId in states)
                {
                    if (this.unitOfWork.ActualRepository.Count(a => a.State == stateId) == 0)
                    {
                        if (this.unitOfWork.EstimateRepository.Count(a => a.State == stateId) == 0)
                        {
                            return new ServiceResult()
                            {
                                IsSuccess = false,
                                Message = "Invalid state id(s).",
                            };
                        }
                        else
                        {
                            populationOfStates.Add(this.unitOfWork.EstimateRepository.GetTotalPopulationByState(stateId));
                        }
                    }
                    else
                    {
                        populationOfStates.Add(this.unitOfWork.ActualRepository.GetSingleOrDefault(a => a.State == stateId).Population);
                    }
                }

                List<StatePopulationViewModel> populations = new List<StatePopulationViewModel>();
                for (int i = 0; i < states.Length; i++)
                {
                    populations.Add(new StatePopulationViewModel()
                    {
                        State = states[i],
                        Population = populationOfStates[i],
                    });
                }

                return new ServiceResult()
                {
                    IsSuccess = true,
                    Data = populations,
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }
    }
}

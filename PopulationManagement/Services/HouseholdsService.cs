// <copyright file="HouseholdsService.cs" company="Placeholder Company">
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
    /// Access Household data.
    /// </summary>
    public class HouseholdsService : IHouseholdsService
    {
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="HouseholdsService"/> class.
        /// </summary>
        /// <param name="unitOfWork">Unit of work instance.</param>
        public HouseholdsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get Household of many states in one time.
        /// </summary>
        /// <param name="states">List of state id.</param>
        /// <returns>List of states and it's Household if exists, otherwise error.</returns>
        public ServiceResult GetHouseholdsOfManyStates(int[] states)
        {
            try
            {
                List<decimal> householdsOfStates = new List<decimal>();
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
                            householdsOfStates.Add(this.unitOfWork.EstimateRepository.GetTotalHouseholdsByState(stateId));
                        }
                    }
                    else
                    {
                        householdsOfStates.Add(this.unitOfWork.ActualRepository.GetSingleOrDefault(a => a.State == stateId).Households);
                    }
                }

                List<StateHouseholdsViewModel> households = new List<StateHouseholdsViewModel>();
                for (int i = 0; i < states.Length; i++)
                {
                    households.Add(new StateHouseholdsViewModel()
                    {
                        State = states[i],
                        Households = householdsOfStates[i],
                    });
                }

                return new ServiceResult()
                {
                    IsSuccess = true,
                    Data = households,
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

namespace API.Controllers
{
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using System;


    /// <summary>
    /// Api controller for managing population and households.
    /// </summary>
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class ResidentController : ControllerBase
    {
        private readonly IPopulationService populationService;
        private readonly IHouseholdsService householdsService;
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger("MyLogger");

        /// <summary>
        /// Initializes a new instance of the <see cref="ResidentController"/> class.
        /// </summary>
        /// <param name="populationService">Population services supplier.</param>
        /// <param name="householdsService">Households services supplier.</param>
        public ResidentController(IPopulationService populationService, IHouseholdsService householdsService)
        {
            this.populationService = populationService;
            this.householdsService = householdsService;
        }

        /// <summary>
        /// Get population by state ids.
        /// </summary>
        /// <param name="state">State ids.</param>
        /// <returns>List of states and its populations.</returns>
        [HttpGet]
        [Route("population")]
        public IActionResult GetPopulations(string state)
        {
            logger.Info(DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss.fff") + " - API endpoint called - /population?state=" + state);

            if (string.IsNullOrEmpty(state))
            {
                return this.BadRequest("State ids is null.");
            }

            var states = state.Split(',');
            int[] stateIds = new int[states.Length];
            for (int i = 0; i < states.Length; i++)
            {
                if (int.TryParse(states[i], out int id))
                {
                    stateIds[i] = id;
                }
                else
                {
                    return this.BadRequest("State id must be intergers.");
                }
            }

            var serviceResult = this.populationService.GetPopulationOfManyStates(stateIds);
            if (!serviceResult.IsSuccess)
            {
                return this.NotFound(serviceResult.Message);
            }

            return this.Ok(serviceResult.Data);
        }

        /// <summary>
        /// Get puopulation by state ids.
        /// </summary>
        /// <param name="state">State ids.</param>
        /// <returns>List of states and its Households.</returns>
        [HttpGet]
        [Route("households")]
        public IActionResult GetHouseholds(string state)
        {
            logger.Info(DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss.fff") + " - API endpoint called - /households?state=" + state);

            if (string.IsNullOrEmpty(state))
            {
                return this.BadRequest("State ids is null.");
            }

            var states = state.Split(',');
            int[] stateIds = new int[states.Length];
            for (int i = 0; i < states.Length; i++)
            {
                if (int.TryParse(states[i], out int id))
                {
                    stateIds[i] = id;
                }
                else
                {
                    return this.BadRequest("State id must be intergers.");
                }
            }

            var serviceResult = this.householdsService.GetHouseholdsOfManyStates(stateIds);
            if (!serviceResult.IsSuccess)
            {
                return this.NotFound(serviceResult.Message);
            }

            return this.Ok(serviceResult.Data);
        }
    }
}

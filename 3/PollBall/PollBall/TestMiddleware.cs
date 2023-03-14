using PollBall.Services;
using System.Globalization;

namespace PollBall
{
    public class TestMiddleware
    {
        private readonly RequestDelegate _next;

        public TestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IPollResultsService pollResultsService)
        {
            if (context.Request.Query.ContainsKey("favorite"))
            {
                var selectedValue = context.Request.Query["favorite"];
                var selectedGame = (SelectedGame)Enum.Parse(typeof(SelectedGame), selectedValue, true);
                pollResultsService.AddVote(selectedGame);
                var gameVotes = pollResultsService.GetVoteResult();
                foreach (var vote in gameVotes)
                {
                    await context.Response.WriteAsync($"<div> Game name: {vote.Key}. Votes: {vote.Value} </div>");
                }
                /*await context.Response.WriteAsync($"Selected value is: {selectedValue}");*/
                //await _next.Invoke(context);
            }
            else await _next.Invoke(context);
        }
    }
}

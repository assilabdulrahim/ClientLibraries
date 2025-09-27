using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Google.GenerativeAI.OpenAi
{
    using Google.GenerativeAI.Contracts;

    internal class OpenAiClient : IGeminiClient
    {
        public Task<string?> GetExpandedConceptsAsync(string prompt)
        {
            throw new NotImplementedException();
        }
    }
}

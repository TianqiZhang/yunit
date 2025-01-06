// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;
using Xunit;

namespace Yunit.Test
{
    public class LLMTestExamples
    {
        private const string ApiKey = "your-api-key-here";

        [Fact]
        public async Task TestNaturalLanguageMatch()
        {
            var test = new LLMTestAttribute(ApiKey);
            await test.VerifyNaturalLanguageAsync(
                "The response should express gratitude and mention a specific feature",
                "Thank you for your feedback about our new search functionality!"
            );
        }

        [Fact]
        public async Task TestStringRuleMatch()
        {
            var test = new LLMTestAttribute(ApiKey);
            await test.VerifyStringRuleAsync(
                @"\b[A-Z]{2,}\b", // Matches words in ALL CAPS
                "This is an IMPORTANT message about SECURITY"
            );
        }

        [Fact]
        public async Task TestLLMEvaluation()
        {
            var test = new LLMTestAttribute(ApiKey);
            await test.VerifyWithLLMAsync(
                "Does this text contain a polite greeting?",
                "Hello, thank you for contacting us!"
            );
        }
    }
}

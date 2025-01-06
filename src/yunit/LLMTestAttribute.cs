// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading.Tasks;

namespace Yunit
{
    /// <summary>
    /// Attribute for LLM-based test assertions
    /// </summary>
    public class LLMTestAttribute : YamlTestAttribute
    {
        private readonly LLMTestConfig _config;

        public LLMTestAttribute(string apiKey, string glob = null) : base(glob)
        {
            _config = new LLMTestConfig(apiKey);
            _config.Validate();
        }

        public async Task VerifyNaturalLanguageAsync(string pattern, string text)
        {
            using var evaluator = new LLMEvaluator(_config);
            var result = evaluator.EvaluateNaturalLanguage(pattern, text);
            if (!result.IsMatch)
            {
                throw new JsonDiffException("Natural language match failed", result.Diff);
            }
        }

        public async Task VerifyStringRuleAsync(string rule, string text)
        {
            using var evaluator = new LLMEvaluator(_config);
            var result = evaluator.EvaluateStringRule(rule, text);
            if (!result.IsMatch)
            {
                throw new JsonDiffException("String rule match failed", result.Diff);
            }
        }

        public async Task VerifyWithLLMAsync(string prompt, string text)
        {
            using var evaluator = new LLMEvaluator(_config);
            var result = await evaluator.EvaluateWithLLM(prompt, text);
            if (!result.IsMatch)
            {
                throw new JsonDiffException("LLM evaluation failed", result.Diff);
            }
        }
    }
}

// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace Yunit
{
    /// <summary>
    /// Extends JsonDiff with LLM-based text evaluation capabilities
    /// </summary>
    public class LLMDiff : JsonDiff
    {
        private readonly LLMEvaluator _evaluator;

        public LLMDiff(LLMEvaluator evaluator)
        {
            _evaluator = evaluator ?? throw new ArgumentNullException(nameof(evaluator));
        }

        /// <summary>
        /// Validates text against natural language criteria
        /// </summary>
        public void VerifyNaturalLanguage(string expectedPattern, string actualText, string summary = null)
        {
            var result = _evaluator.EvaluateNaturalLanguage(expectedPattern, actualText);
            if (!result.IsMatch)
            {
                throw new JsonDiffException(summary, result.Diff);
            }
        }

        /// <summary>
        /// Validates text against string pattern rules
        /// </summary>
        public void VerifyStringRule(string rulePattern, string actualText, string summary = null)
        {
            var result = _evaluator.EvaluateStringRule(rulePattern, actualText);
            if (!result.IsMatch)
            {
                throw new JsonDiffException(summary, result.Diff);
            }
        }

        /// <summary>
        /// Evaluates text using LLM with custom prompt
        /// </summary>
        public void VerifyWithLLM(string prompt, string actualText, string summary = null)
        {
            var result = _evaluator.EvaluateWithLLM(prompt, actualText);
            if (!result.IsMatch)
            {
                throw new JsonDiffException(summary, result.Diff);
            }
        }
    }
}

// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Yunit
{
    /// <summary>
    /// Configuration settings for LLM-based testing
    /// </summary>
    public class LLMTestConfig
    {
        public string ApiKey { get; set; }
        public string Model { get; set; } = "gpt-3.5-turbo";
        public double Temperature { get; set; } = 0.7;
        public int MaxTokens { get; set; } = 1000;
        public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(30);

        public LLMTestConfig(string apiKey)
        {
            ApiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
        }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(ApiKey))
            {
                throw new ArgumentException("API key is required");
            }

            if (Temperature < 0 || Temperature > 2)
            {
                throw new ArgumentException("Temperature must be between 0 and 2");
            }

            if (MaxTokens < 1 || MaxTokens > 4000)
            {
                throw new ArgumentException("Max tokens must be between 1 and 4000");
            }

            if (Timeout.TotalSeconds < 1 || Timeout.TotalSeconds > 300)
            {
                throw new ArgumentException("Timeout must be between 1 and 300 seconds");
            }
        }
    }
}

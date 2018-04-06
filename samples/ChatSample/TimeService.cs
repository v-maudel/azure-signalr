﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Internal.Protocol;
using Microsoft.Azure.SignalR;

namespace ChatSample
{
    public class TimeService
    {
        private readonly HubProxy _hubProxy;
        private readonly Timer _timer;
        private readonly JsonHubProtocol _jsonProtocol;
        private readonly MessagePackHubProtocol _msgpackProtocol;

        public TimeService(HubProxy hubProxy)
        {
            _hubProxy = hubProxy ?? throw new ArgumentNullException(nameof(hubProxy));
            _timer = new Timer(Run, this, 100, 60 * 1000);
            _jsonProtocol = new JsonHubProtocol();
            _msgpackProtocol = new MessagePackHubProtocol();
        }

        private static void Run(object state)
        {
            _ = ((TimeService)state).Broadcast();
        }

        private async Task Broadcast()
        {
            var hubMessage = CreateInvocationMessage("broadcastMessage",
                    new object[] { "_BROADCAST_", DateTime.UtcNow.ToString(CultureInfo.InvariantCulture) });

            await _hubProxy.Clients.All.SendAsync(_jsonProtocol.WriteToArray(hubMessage),
                _msgpackProtocol.WriteToArray(hubMessage));
        }

        private InvocationMessage CreateInvocationMessage(string methodName, object[] args)
        {
            return new InvocationMessage(target: methodName, argumentBindingException: null, arguments: args);
        }
    }
}

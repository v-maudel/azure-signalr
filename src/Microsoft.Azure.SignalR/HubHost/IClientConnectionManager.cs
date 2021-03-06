﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Concurrent;

namespace Microsoft.Azure.SignalR
{
    internal interface IClientConnectionManager
    {
        void AddClientConnection(ServiceConnectionContext clientConnection);

        void RemoveClientConnection(string connectionId);

        ConcurrentDictionary<string, ServiceConnectionContext> ClientConnections { get; }
    }
}

using System;
using System.Collections.Generic;
using ClassIsland.Core.Abstractions.Services;
using ClassIsland.Core.Enums.Notification;
using ClassIsland.Core.Models.Notification;
using ClassIsland.Core.Abstractions;

namespace ClassIsland.Services;

/// <summary>
/// 通过 <see cref="INotificationBus"/> 委托事件
/// </summary>
public class NotificationBus : INotificationBus
{
    /// <inheritdoc />
    public event Action<NotificationRequest, NotificationState, NotificationState>? StateChanged;

    /// <inheritdoc />
    public event Func<INotificationConsumer, IList<NotificationPlayingTicket>>? PullRequested;

    /// <inheritdoc />
    public event Action<INotificationConsumer>? ConsumerRemoved;

    /// <inheritdoc />
    public event Action? DispatchRequested;

    /// <inheritdoc />
    public void RaiseStateChanged(NotificationRequest request, NotificationState from, NotificationState to)
    {
        StateChanged?.Invoke(request, from, to);
    }

    /// <inheritdoc />
    public IList<NotificationPlayingTicket> RaisePullRequested(INotificationConsumer consumer)
    {
        return PullRequested?.Invoke(consumer) ?? [];
    }

    /// <inheritdoc />
    public void RaiseConsumerRemoved(INotificationConsumer consumer)
    {
        ConsumerRemoved?.Invoke(consumer);
    }

    /// <inheritdoc />
    public void RaiseDispatchRequested()
    {
        DispatchRequested?.Invoke();
    }
}

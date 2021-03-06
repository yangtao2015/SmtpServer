using System;
using System.Net;
using SmtpServer.Protocol;

namespace SmtpServer
{
    internal class SmtpSessionContext : ISmtpSessionContext
    {
        /// <summary>
        /// Fired when a command is about to execute.
        /// </summary>
        public event EventHandler<SmtpCommandExecutingEventArgs> CommandExecuting;

        bool _isQuitRequested;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="transaction">The SMTP transaction.</param>
        /// <param name="stateMachine">The current state machine for the session.</param>
        /// <param name="remoteEndPoint">The remote endpoint of the client making the connection.</param>
        internal SmtpSessionContext(ISmtpTransaction transaction, ISmtpStateMachine stateMachine, EndPoint remoteEndPoint)
        {
            Transaction = transaction;
            StateMachine = stateMachine;
            RemoteEndPoint = remoteEndPoint;
        }

        /// <summary>
        /// Indicates to the session that it should quit.
        /// </summary>
        public void Quit()
        {
            _isQuitRequested = true;
        }

        /// <summary>
        /// Raise the SMTP command as executing.
        /// </summary>
        /// <param name="command">The command that is executing.</param>
        internal void RaiseSmtpCommandExecuting(SmtpCommand command)
        {
            CommandExecuting?.Invoke(this, new SmtpCommandExecutingEventArgs(command));
        }

        /// <summary>
        /// Gets or sets the text stream to read from and write to.
        /// </summary>
        public ITextStream Text { get; set; }

        /// <summary>
        /// The transfer encoding that is required for the message.
        /// </summary>
        public ContentEncoding TransferEncoding { get; set; }

        /// <summary>
        /// Gets the current transaction.
        /// </summary>
        public ISmtpTransaction Transaction { get; }

        /// <summary>
        /// Gets the current state machine.
        /// </summary>
        public ISmtpStateMachine StateMachine { get; }

        /// <summary>
        /// Gets the remote endpoint of the client.
        /// </summary>
        public EndPoint RemoteEndPoint { get; }

        /// <summary>
        /// Gets a value indicating whether a quit has been requested.
        /// </summary>
        public bool IsQuitRequested
        {
            get { return _isQuitRequested; }
        }
    }
}
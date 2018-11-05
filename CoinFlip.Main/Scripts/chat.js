$(function () {

    var chat = $.connection.chatHub;

    chat.client.addNewMessageToPage = function (message) {
        // Add the message to the page.
        $('#chat_box').append('<li class="chat-message-column"><span class="chat-img pull-left"><img alt= "" class="chat-message-pic" src= "https://steamcdn-a.akamaihd.net/steamcommunity/public/images/avatars/51/518113aa1ca8373c57ddbcf86460cab54547dddf.jpg"/></span ><div class="chat-text-body"><div class="header"><strong class="chat-message-name text-muted">Kaunet</strong></div>' + message + '</div></li > ');
    };


    // Set initial focus to message input box.
    $('#chat_message').focus();
    // Start the connection.
    $.connection.hub.start().done(function ()
    {
        $('#chat_send_message_btn').click(function ()
        {
            var message = $('#chat_message').val();

            if (message.length !== 0)
            {
                // Call the Send method on the hub.
                chat.server.send(message);
                // Clear text box and reset focus for next comment.
                $('#chat_message').val('').focus();
            }
        });
    });
});
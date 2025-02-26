function loadComments(projectId) {
    $.ajax({
        url: '/ProjectManagement/ProjectComment/GetComments?projectId=' + projectId,
        method: 'GET',
        success: function (data) {
            var commentHtml = '';
            data.forEach(function (comment) {
                commentHtml += '<div class="comment mb-3 p-3 shadow-sm" style="background-color: #f8f9fa; border-radius: 10px;">';
                commentHtml += '<p class="mb-2" style="font-size: 1.1rem;">' + comment.content + '</p>';
                commentHtml += '<span class="text-muted" style="font-size: 0.9rem;">Posted on ' + new Date(comment.datePosted).toLocaleString() + '</span>';
                commentHtml += '</div>';
            });
            $('#commentList').html(commentHtml);
        },
        error: function (xhr, status, error) {
            console.log("Error: " + error);
        }
    });
}

$(document).ready(function () {
    var projectId = $('#projectComments input[name="ProjectId"]').val();
    loadComments(projectId);

    $('#addCommentForm').submit(function (e) {
        e.preventDefault();
        var formData = JSON.stringify({
            ProjectId: projectId,
            Content: $('textarea[name="Content"]').val()
        });

        $.ajax({
            url: '/ProjectManagement/ProjectComment/AddComment',
            method: 'POST',
            contentType: 'application/json',
            data: formData,
            success: function (response) {
                if (response.success) {
                    $('textarea[name="Content"]').val('');
                    loadComments(projectId);
                } else {
                    alert(response.message);
                }
            },
            error: function (xhr, status, error) {
                alert("Error: " + error);
            }
        });
    });
});

function fetchdata(commentsSection,productId) {


    productId = productId.value;
    commentsSection = commentsSection.getElementsByClassName('total-comment')[0];

  /*  let url = `/Comment/GetAllByAd?id=${productId}`;*/


    fetch(`/Comment/GetAllByAd?id=${productId}`)
      .then(response => response.json())
      .then(comments => displayComments(comments, commentsSection));

}


function displayComments(comments, commentsSection)
{
    if (comments.length === 0) {
        let div = document.createElement('div');
        div.innerHTML = '<h2 class="text-center">No Comments</h2>';

        commentsSection.appendChild(div);
    } else {
        for (let comment of comments) {
            let writtenOn = new Date(`${comment.writtenOn} UTC`);
            let writtenOnLocalTime = getLocalDataTimeString(writtenOn);

            commentsSection.innerHTML +=
                `<div class="single-comment">
                <div class="user-details d-flex align-items-center flex-wrap">
                    <div class="user-name order-3 order-sm-2">
                        <h5><b>${comment.username}</b></h5>
                        <span>${writtenOnLocalTime}</span>
                    </div>
                </div>

                <p class="user-comment">
                    ${comment.text}
                </p>
            </div>`;
        }
    }
}

function getLocalDataTimeString(date) {
    var dateLocalTime = date.toString();

    let month = dateLocalTime.split(' ')[1];
    let day = dateLocalTime.split(' ')[2];
    let year = dateLocalTime.split(' ')[3];
    let time = dateLocalTime.split(' ')[4];

    let hours = time.split(':')[0];
    let minutes = time.split(':')[1];

    return `${day} ${month} ${year} ${hours}:${minutes}`;
}



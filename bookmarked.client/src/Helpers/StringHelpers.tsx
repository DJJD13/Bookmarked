export const formatAuthorName = (author: string) : string => {
    if(!author.includes(",")) {
        return author;
    } else {
        const authArr = author.split(",");
        return authArr[1] + " " + authArr[0];
    }
}

export const removeHTMLTags = (html: string) : string => {
    const tempDiv = document.createElement("div");
    tempDiv.innerHTML = html;
    return tempDiv.innerText;
}
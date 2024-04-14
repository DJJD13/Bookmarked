export const formatAuthorName = (author: string) : string => {
    if(!author.includes(",")) {
        return author;
    } else {
        const authArr = author.split(",");
        return authArr[1] + " " + authArr[0];
    }
}
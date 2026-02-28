const Users = [
    { id: 1, name: "Ali" },
    { id: 2, name: "Sara" }
];

const Posts = [
    { id: 1, userId: 1, title: "JS Basics" },
    { id: 2, userId: 1, title: "Async Await" },
    { id: 3, userId: 2, title: "HTML Tips" }
];

async function fetchData() {
    return new Promise((resolve) => {
        setTimeout(() => {
            resolve({ Users, Posts });
        }, 500);
    });
}

async function searchUserById(data, userId) {
    return new Promise((resolve, reject) => {
        const foundUser = data.Users.find(u => u.id === userId);
        if (foundUser)
            resolve(foundUser);
        else
            reject("User not found");

    });
}

async function searchPostById(data, userId, postId) {
    return new Promise((resolve, reject) => {
        const foundPost = data.Posts.find(p => p.id === postId && p.userId === userId);
        if (foundPost)
            resolve(foundPost);
        else
            reject("Post not found or does not belong to the user");
    });
}

async function showUserPost(userId, postId) {

    const data = await fetchData();
    const user = await searchUserById(data, userId);
    const post = await searchPostById(data, userId, postId);
    return { user, post };
}

showUserPost(1, 3)
    .then(({ user, post }) => {
        console.log(`User Name: ${user.name}`);
        console.log(`Post Title: ${post.title}`);
    })
    .catch((error) => {
        console.error(error);
    });

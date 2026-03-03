let integersArr = [1, 2, 3, 4, 5];

let copyIntergerArr = [...integersArr];

copyIntergerArr[0] = 100;
copyIntergerArr[3] = 102;

console.log("Original Array:", integersArr);
console.log("Copied Array:", copyIntergerArr);


let objectsArr = [{ a: 1 }, { b: 2 }, { c: 3 }];

let copyObjectsArr = [...objectsArr];

copyObjectsArr[0].a = 1001;
copyObjectsArr[2].a = 1005;

 copyObjectsArr.push({ d: 6 });

console.log("Original Objects Array:", objectsArr);
console.log("Copied Objects Array:", copyObjectsArr);

// try push live ...
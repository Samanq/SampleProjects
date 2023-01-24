let student: {
  readonly id: number; // This property is readonly and user cannot change it's value.
  name: string;
  nickName?: string; // This property is optional.
  study: (subjectTitle: string, duration: number) => void;
} = {
  id: 1,
  name: "john",
  nickName: "johny",
  study: (subjectTitle: string, duration: number) => {
    console.log("is studying " + subjectTitle + " for " + duration + " hours.");
  },
}; // Initializing the object.

// Using the object.
student.name = "John";
student.study("C#", 2);
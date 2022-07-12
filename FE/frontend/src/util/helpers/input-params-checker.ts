export const inputParamsChecker = (inputParams: string[]) => {
  let array: string[] = [];
  inputParams.forEach((value) => {
    if (value !== '') {
      array = [...array, value];
    }
  });
  return array;
};

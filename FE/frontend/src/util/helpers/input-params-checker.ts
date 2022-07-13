export const inputParamsChecker = (inputParams: string[]) => {
  return inputParams.filter((value) => {
    return value !== '';
  });
};

export const findPosition = (payload: number) => {
  switch (payload) {
    case 0:
      return 'Business Operations';
    case 1:
      return 'Cybersecurity';
    case 2:
      return 'Data Science and Analytics';
    case 3:
      return 'Engineering';
    case 4:
      return 'Information Technology';
    case 5:
      return 'Manufacturing';
    default:
      return 'User';
  }
};

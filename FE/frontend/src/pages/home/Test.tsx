import { FC, useState } from 'react';
import { useAuthStateValue } from '../../context/AuthContext';

const Test: FC = () => {
  const { isAuthenticated, testMessage } = useAuthStateValue();
  console.log('Test');
  console.log(isAuthenticated);

  return (
    <>
      <div>Test</div>
      <div>{testMessage}</div>
    </>
  );
};

export default Test;

import { FC } from 'react';

const Error: FC<{ message: string }> = ({ message }) => {
  return (
    <>
      <h1>Error page</h1>
      <h1>{message}</h1>
    </>
  );
};

export default Error;

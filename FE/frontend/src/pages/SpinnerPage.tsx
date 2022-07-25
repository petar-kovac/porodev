import { FC } from 'react';

import styled from 'styled-components';

import Spinner from 'components/spinner/Spinner';

interface ISpinnerProps {
  color?: any;
  size?: any;
  speed?: any;
}

const SpinnerPage: FC<ISpinnerProps> = ({ color, size, speed }) => {
  return (
    <StyledPage>
      <SpinnerWrapper>
        <Spinner color="#000" size={42} speed={2} />
      </SpinnerWrapper>
    </StyledPage>
  );
};

export default SpinnerPage;

const SpinnerWrapper = styled.div`
  height: 200px;
  width: 200px;
  display: flex;
  justify-content: center;
  align-items: center;
`;

const StyledPage = styled.div`
  height: 100vh;
  width: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
  background-color: #f0f2f5;
`;

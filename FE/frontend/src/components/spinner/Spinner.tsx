import { Airplane } from '@styled-icons/ionicons-outline';
import { Spin } from 'antd';
import { FC } from 'react';
import styled from 'styled-components';

interface ISpinnerProps {
  size: number;
  color: string;
  speed: number;
}

const Spinner: FC<ISpinnerProps> = ({ size, color, speed }) => {
  return (
    <div>
      <StyledSpin
        speed={speed}
        indicator={<StyledIcon color={color} size={size} />}
      />
    </div>
  );
};

export default Spinner;

const StyledSpin = styled(Spin).attrs({
  'data-testid': 'spinner',
})<{ speed: number }>`
  animation: rotation ${(props) => props.speed}s infinite linear;
  @keyframes rotation {
    from {
      transform: rotate(0deg);
    }
    to {
      transform: rotate(359deg);
    }
  }
`;

const StyledIcon = styled(Airplane)<{ size: number; color: string }>`
  font-size: ${(props) => props.size}px;
  color: ${(props) => props.color};
`;

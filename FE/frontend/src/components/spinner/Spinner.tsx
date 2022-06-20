import { FC } from 'react';
import { StyledIcon, StyledSpin } from 'styles/icons/styled-icons';

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

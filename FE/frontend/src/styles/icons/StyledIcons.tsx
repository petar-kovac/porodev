import { FC } from 'react';
import { StyledFile, StyledFolder } from './styled-icons';

/**
 * Component to render icon based on icon type
 */
const StyledIcon: FC<{ type?: string }> = ({ type }) => {
  switch (type) {
    case 'folder':
      return <StyledFolder />;

    case 'file':
      return <StyledFile />;

    default:
      return null;
  }
};

export default StyledIcon;

import { Layout } from 'antd';
import { FC } from 'react';
import styled from 'styled-components';

import PDropdown from 'components/dropdown/Dropdown';
import { StorageKey } from 'util/enums/enums';
import logoImage from 'assets/logo.png';

const { Header } = Layout;

const PHeader: FC = () => {
  return (
    <StyledHeader style={{ color: 'white' }}>
      <StyledLogo src={logoImage} />
      <StyledButtons>
        <StyledUserName>{localStorage.getItem(StorageKey.NAME)}</StyledUserName>
        <StyledUserName>
          {localStorage.getItem(StorageKey.LASTNAME)}
        </StyledUserName>
        <PDropdown />
      </StyledButtons>
    </StyledHeader>
  );
};

const StyledUserName = styled.div`
  font-size: 20px;
  font-weight: 500;
`;
const StyledButtons = styled.div`
  display: flex;
  align-items: center;
  gap: 10px;
`;
const StyledHeader = styled(Header)`
  background-color: #1990ff;
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding-right: 20px;
  padding-left: 10px;
`;
const StyledLogo = styled.img`
  width: 110px;
`;

export default PHeader;

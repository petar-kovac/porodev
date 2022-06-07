import { Layout } from 'antd';
import { FC } from 'react';
import styled from 'styled-components';
import PDropdown from '../../components/dropdown/Dropdown';
import { StorageKey } from '../../util/enums/enums';

const { Header } = Layout;

const PHeader: FC = () => {
  return (
    <StyledHeader style={{ color: 'white' }}>
      <StyledLogo>Header</StyledLogo>
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
  padding: 0 20px;
`;
const StyledLogo = styled.div`
  color: #fff;
  font-size: 20px;
  font-weight: 500;
`;

export default PHeader;

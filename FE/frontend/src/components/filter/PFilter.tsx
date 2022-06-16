import {
  AppstoreOutlined,
  BarsOutlined,
  FolderFilled,
} from '@ant-design/icons';
import { DatePicker, Select, Slider, Card } from 'antd';
import { Dispatch, FC, SetStateAction } from 'react';
import styled from 'styled-components';

const { RangePicker } = DatePicker;
const { Option } = Select;

interface IPFilterProps {
  isList: boolean;
  setIsList: Dispatch<SetStateAction<boolean>>;
}

const PFilter: FC<IPFilterProps> = ({ isList, setIsList }) => {
  const handleChange = (value: string) => {
    console.log(`selected ${value}`);
  };
  return (
    <StyledFilesHeader>
      <StyledFilesDateFilter>
        <h4>Filter files by date:</h4>
        <RangePicker />
      </StyledFilesDateFilter>

      <StyledFilesSlider>
        <h4>Filter by size:</h4>
        <Slider range defaultValue={[0, 30]} />
      </StyledFilesSlider>
      <StyledFilesSelect>
        <Select defaultValue="Filter by type" onChange={handleChange}>
          <Option value="jpg">.jpg</Option>
          <Option value="png">.png</Option>
          <Option value="txt">.txt</Option>
          <Option value="pdf">.pdf</Option>
        </Select>
        <Select defaultValue="Sort by" onChange={handleChange}>
          <Option value="asc">Ascending</Option>
          <Option value="desc">Descending</Option>
          <Option value="newest" disabled>
            Newest
          </Option>
          <Option value="oldest">Oldest</Option>
        </Select>
        <StyledToggleIcons>
          {isList ? (
            <StyledAppstoreOutlined onClick={() => setIsList(!isList)} />
          ) : (
            <StyledBarsOutlined onClick={() => setIsList(!isList)} />
          )}
        </StyledToggleIcons>
      </StyledFilesSelect>
    </StyledFilesHeader>
  );
};

const StyledFilesHeader = styled.div`
  width: 100%;
  display: flex;
  align-items: flex-end;
  gap: 1rem;
  justify-content: space-around;
  flex-wrap: wrap;
  padding: 2rem 0;
  background-color: #fcfcfc;
  border-radius: 3rem;
  box-shadow: 0 1px #ffffff inset, 1px 3px 8px rgba(34, 25, 25, 0.2);
  border-bottom: 2px solid #ddd;
`;

const StyledToggleIcons = styled.div`
  margin-left: auto;
`;
const StyledFilesSlider = styled.div`
  width: 32rem;
  .ant-slider {
    margin: 2.2rem 0.6rem 1rem;
    padding: 0 !important;
  }
`;

const StyledFilesDateFilter = styled.div`
  width: 32rem;
  display: flex;
  flex-direction: column;
  justify-content: space-between;

  .ant-picker {
    border-radius: 1rem;
  }
`;

const StyledFilesSelect = styled.div`
  width: 32rem;
  align-self: flex-end;
  display: flex;
  gap: 1rem;
  align-items: center;

  .ant-select-selector {
    border-radius: 10px !important;
    color: #999;
  }
`;

const StyledAppstoreOutlined = styled(AppstoreOutlined)`
  font-size: 2rem;
  cursor: pointer;
  color: #555;
`;

const StyledBarsOutlined = styled(BarsOutlined)`
  font-size: 2rem;
  cursor: pointer;
  color: #555;
`;

export default PFilter;

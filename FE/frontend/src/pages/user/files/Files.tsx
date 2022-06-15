import axios from 'axios';
import PCard from 'components/card/PCard';
import { FC, useCallback, useEffect, useRef, useState } from 'react';
import styled from 'styled-components';

import { Button, DatePicker, Modal, Select, Slider } from 'antd';
import { AppstoreOutlined, BarsOutlined } from '@ant-design/icons';

import PModal from 'components/modal/PModal';
import PFileSider from 'layout/sider/PFileSider';
import ListCard from 'components/card/ListCard';

const { RangePicker } = DatePicker;
const { Option } = Select;

interface IModalData {}

const Files: FC = () => {
  const [data, setData] = useState<[]>([]);
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [cardData, setCardData] = useState<any>();
  const [isSiderVisible, setIsSiderVisible] = useState(false);
  const [isList, setIsList] = useState<boolean>(false);

  const count = useRef(0);

  const onClick = useCallback((value: any) => {
    count.current += 1;
    setCardData(value);
    setTimeout(() => {
      if (count.current === 1) {
        setIsSiderVisible(true);
      }

      if (count.current === 2) {
        setIsSiderVisible(false);
        setIsModalVisible(true);
      }

      count.current = 0;
    }, 300);
  }, []);

  const handleChange = (value: string) => {
    console.log(`selected ${value}`);
  };

  useEffect(() => {
    const fetchFiles = async () => {
      try {
        const res = await axios.get(`${process.env.REACT_APP_MOCK_URL}/files`);
        setData(res.data);
      } catch (err) {
        console.log(err);
      }
    };
    fetchFiles();
  }, []);

  return (
    <StyledPageWrapper>
      <StyledContent>
        <StyledFilesHeader
          style={{
            width: '70vw',
            margin: '4rem auto',
            display: 'flex',
            alignItems: 'flex-end',
            gap: '10px',
            justifyContent: 'space-around',
            flexWrap: 'wrap',
          }}
        >
          <StyledFilesDateFilter>
            <h4>Filter files by date:</h4>
            <RangePicker />
          </StyledFilesDateFilter>

          <StyledFilesSlider>
            <h4>Filter by size:</h4>
            <Slider style={{ width: '300px' }} range defaultValue={[0, 30]} />
          </StyledFilesSlider>
          <StyledFilesSelect>
            <Select
              defaultValue="Filter by type"
              style={{ width: 160 }}
              onChange={handleChange}
            >
              <Option value="jpg">.jpg</Option>
              <Option value="png">.png</Option>
              <Option value="txt">.txt</Option>
              <Option value="pdf">.pdf</Option>
            </Select>
            <Select
              defaultValue="Sort by"
              style={{ width: 160 }}
              onChange={handleChange}
            >
              <Option value="asc">Ascending</Option>
              <Option value="desc">Descending</Option>
              <Option value="newest" disabled>
                Newest
              </Option>
              <Option value="oldest">Oldest</Option>
            </Select>
          </StyledFilesSelect>
          <div>
            {isList ? (
              <AppstoreOutlined
                style={{ fontSize: '2rem', cursor: 'pointer' }}
                onClick={() => setIsList(!isList)}
              />
            ) : (
              <BarsOutlined
                style={{ fontSize: '2rem', cursor: 'pointer' }}
                onClick={() => setIsList(!isList)}
              />
            )}
          </div>
        </StyledFilesHeader>
        <StyledFilesWrapper>
          {data?.map((value: any, index: number) => (
            <>
              {isList ? (
                <ListCard
                  image={value?.image}
                  heading={value?.name}
                  description={value?.description}
                  onClick={() => onClick(value)}
                />
              ) : (
                <PCard
                  image={value?.image}
                  heading={value?.name}
                  description={value?.description}
                  onClick={() => onClick(value)}
                />
              )}
            </>
          ))}
        </StyledFilesWrapper>
        <PFileSider
          title={cardData?.name}
          content={cardData?.description}
          isSiderVisible={isSiderVisible}
          setIsSiderVisible={setIsSiderVisible}
        />
      </StyledContent>

      <PModal
        isModalVisible={isModalVisible}
        title={cardData?.name}
        content={cardData?.description}
        setIsModalVisible={setIsModalVisible}
      />
    </StyledPageWrapper>
  );
};

const StyledContent = styled.div`
  display: flex;
`;
const StyledPageWrapper = styled.div`
  display: flex;
  flex-direction: column;
`;
const StyledFilesWrapper = styled.div`
  padding: 20px;
  display: flex;
  justify-content: center;
  gap: 25px;
  flex-wrap: wrap;
`;

const StyledFilesHeader = styled.div`
  width: 100%;
  padding: 2rem 0;
  background-color: #fcfcfc;
  border-radius: 30px;
  box-shadow: 0 1px #ffffff inset, 1px 3px 8px rgba(34, 25, 25, 0.2);
  margin-bottom: 5rem;
  border-bottom: 2px solid #ddd;
`;

const StyledFilesSlider = styled.div`
  width: 32rem;
  .ant-slider {
    margin: 22px 6px 10px;
    padding: 0 !important;
  }
`;

const StyledFilesModal = styled(Modal)`
  .ant-modal-header {
    border-radius: 1.2rem;
    background-color: rgba(220, 220, 220, 0.1);
    border: 1px solid #fff;
    border-bottom: 1px solid #eee;
  }

  .ant-modal-title {
    color: #555;
    letter-spacing: 0.5px;
    font-size: 1.8rem;
  }

  .ant-modal-content {
    border-radius: 1.2rem;
    box-shadow: 1px 3px 4px rgba(255, 255, 255, 0.4);
  }

  .ant-modal-footer {
    padding: 1.4rem 2.2rem;
    border-radius: 1.6rem;

    .footer-content {
      display: flex;
      justify-content: space-between;
      align-items: center;
    }
  }
`;

const StyledFilesButton = styled(Button)`
  border-radius: 0.8rem;
`;

const StyledFilesDateFilter = styled.div`
  width: 32rem;
  display: flex;
  flex-direction: column;
  justify-content: space-between;

  .ant-picker {
    border-radius: 10px;
  }
`;

const StyledFilesSelect = styled.div`
  width: 32rem;
  align-self: flex-end;
  display: flex;
  gap: 1rem;

  .ant-select-selector {
    border-radius: 10px !important;
    color: #999;
  }
`;

export default Files;

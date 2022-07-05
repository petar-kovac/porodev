import axios from 'axios';
import { FC, ReactNode, useEffect, useState } from 'react';

import RuntimeCard from 'components/card/RuntimeCard';
import PModal from 'components/modal/PModal';
import { usePageContext } from 'context/PageContext';
import PFileSider from 'layout/sider/PFileSider';
import { startRuntimeService } from 'service/runtime/runtime';
import { IFilesCard } from 'types/card-data';
import { GetRuntimeModalData } from 'util/util-components/GetRuntimeModalData';
import {
  StyledContent,
  StyledFilesWrapper,
  StyledPageWrapper,
  StyledStaticContent,
} from './runtime-styled';

const Runtime: FC = () => {
  const [data, setData] = useState<IFilesCard[] | undefined>([]);
  const [cardData, setCardData] = useState<IFilesCard | null>(null);
  const [content, setContent] = useState<ReactNode>(undefined);

  const {
    setIsLoading,
    setIsSiderVisible,
    setIsModalVisible,
    setNumberOfInputFields,
    setInputParameters,
  } = usePageContext();

  const startRuntime = async () => {
    setIsLoading(true);

    try {
      const res = await startRuntimeService({
        projectId: '3fa85f64-5717-4562-b3fc-2c963f66afa6',
        arguments: ['a'],
      });
      const modalDataToRender: ReactNode = GetRuntimeModalData(res);
      setContent(modalDataToRender);
      setIsSiderVisible(false);
      setIsModalVisible(true);
      setIsLoading(false);
    } catch (err) {
      setIsLoading(false);
    }
  };

  useEffect(() => {
    setIsSiderVisible(false);
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
        <StyledStaticContent
          onClick={() => {
            setIsSiderVisible(false);
            setCardData(null); // to trigger rerender, simulating onBlur effect
            setInputParameters([]);
            setNumberOfInputFields(1);
          }}
        >
          <StyledFilesWrapper>
            {data?.slice(0, 3).map((value: any, index) => (
              <RuntimeCard
                key={value.id}
                title={value?.title}
                createdAt={value?.createdAt}
                selected={value?.id === cardData?.id}
                onClick={(e) => {
                  setNumberOfInputFields(1);
                  setInputParameters([]);
                  setCardData(value);
                  e.stopPropagation();
                  setIsSiderVisible(true);
                }}
                onDoubleClick={(e) => {
                  e.stopPropagation();
                  setCardData(value);
                  setIsSiderVisible(false);
                  setIsModalVisible(true);
                }}
              />
            ))}
          </StyledFilesWrapper>
        </StyledStaticContent>

        <PFileSider
          type="runtime"
          onButtonClick={startRuntime}
          cardData={cardData}
          setCardData={setCardData}
        />
      </StyledContent>
      <PModal
        title="Result of you action: "
        cardData={cardData}
        setCardData={setCardData}
        content={content}
      />
    </StyledPageWrapper>
  );
};

export default Runtime;

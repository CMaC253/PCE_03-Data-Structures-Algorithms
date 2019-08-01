<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

	<!-- The exercise name to put at the top of the document -->
	<xsl:template name="LessonNumber">PCE 03</xsl:template>


  <!-- This is for Categories that have a name, but the name doesn't match anything.
			This should never happen 'in production', and will be flagged as an error 
			during the output phase -->
  <xsl:template match="Category[@name!='']" priority="-10">
    <xsl:call-template name="GenerateFailedTest">
      <xsl:with-param name="CategoryName">
        <xsl:value-of select="$Missing_Category"/>
      </xsl:with-param>
      <xsl:with-param name="NodeList" select="." />
      <xsl:with-param name="PointPenalty" select="-1" />
      <xsl:with-param name="Explanation">
        Unable to find a grading category for <xsl:value-of select="@name"/>
      </xsl:with-param>
    </xsl:call-template>
  </xsl:template>

	<xsl:template match="Category[@name='SmartArray Basics']">
		<xsl:call-template name="GenerateFailedTest">
			<xsl:with-param name="CategoryName">
				<xsl:value-of select="@name"/>
			</xsl:with-param>
			<xsl:with-param name="NodeList" select="." />
			<xsl:with-param name="PointPenalty" select="-1" />
			<xsl:with-param name="Explanation">
				There is a problem with your SmartArray and/or your
				Stack, and/or your Queue.  This test fails if any of those fail to
				pass the tests in Test_SmartArray_Basic.  You can tell which one based
				on whether the word in big blue title, above, starts with Test_Queue...,
				Test_SmartArray..., or Test_Stack...
			</xsl:with-param>
		</xsl:call-template>
	</xsl:template>


	<xsl:template match="Category[@name='StackOfInts']">
		<xsl:call-template name="GenerateFailedTest">
			<xsl:with-param name="CategoryName">
				<xsl:value-of select="@name"/>
			</xsl:with-param>
			<xsl:with-param name="NodeList" select="." />
			<xsl:with-param name="PointPenalty" select="-2" />
			<xsl:with-param name="Explanation">
				There is a problem with your "StackOfInts" exercise (specifically with the Stack
				functionality, and not the basic SmartArray functionality)
			</xsl:with-param>
		</xsl:call-template>
	</xsl:template>

	<xsl:template match="Category[@name='QueueOfInts']">
		<xsl:call-template name="GenerateFailedTest">
			<xsl:with-param name="CategoryName">
				<xsl:value-of select="@name"/>
			</xsl:with-param>
			<xsl:with-param name="NodeList" select="." />
			<xsl:with-param name="PointPenalty" select="-2" />
			<xsl:with-param name="Explanation">
				There is a problem with your "QueueOfInts" exercise.(specifically with the Queue
				functionality, and not the basic SmartArray functionality)
			</xsl:with-param>
		</xsl:call-template>
	</xsl:template>

	<xsl:template match="Category[@name='SmartArrayResizable']">
		<xsl:call-template name="GenerateFailedTest">
			<xsl:with-param name="CategoryName">
				<xsl:value-of select="@name"/>
			</xsl:with-param>
			<xsl:with-param name="NodeList" select="." />
			<xsl:with-param name="PointPenalty" select="-3" />
			<xsl:with-param name="Explanation">
				There is a problem with your "SmartArrayResizable" exercise.
			</xsl:with-param>
		</xsl:call-template>
	</xsl:template>

</xsl:stylesheet>

